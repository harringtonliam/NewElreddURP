using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using UnityEngine.Events;
using System;

namespace RPG.Attributes
{

    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] UnityEvent<float> takeDamage;
        [SerializeField] UnityEvent onDie;

        public event Action healthUpdated;

        GameConsole gameConsole;
        CharacterSheet characterSheet;
        string characterName;


        float healthPoints = -1f;

        bool isDead = false;

        public bool IsDead { get { return isDead; } }

        public float HealthPoints {
            get { return healthPoints; }
        }

        void Start()
        {
            gameConsole = FindObjectOfType<GameConsole>();
            characterSheet = GetComponent<CharacterSheet>();
            if (characterSheet != null)
            {
                characterName = characterSheet.CharacterName;
            }

            if (healthPoints < 0)
            {
                healthPoints = GetMaxHealthPoints();
            }
            if (healthUpdated != null)
            {
                healthUpdated();
            }


        }

        private void OnEnable()
        {
            BaseStats baseStats = GetComponent<BaseStats>();
            if (baseStats != null)
            {
                baseStats.onLevelUp += BaseStats_onLevelUp;
            }
        }

        private void OnDisable()
        {
            BaseStats baseStats = GetComponent<BaseStats>();
            if (baseStats != null)
            {
                baseStats.onLevelUp -= BaseStats_onLevelUp;
            }
        }

        public float GetPercentage()
        {
            return ( healthPoints / GetMaxHealthPoints()) * 100;
        }

        public float GetMaxHealthPoints()
        {
            BaseStats baseStats = GetComponent<BaseStats>();
            float maxHealtHPoints = baseStats.GetStat(Stat.Health);
            int abilityModifier = 0;
            CharacterAbilities characterAbilities = GetComponent<CharacterAbilities>();
            if (characterAbilities != null)
            {
                abilityModifier = GetComponent<CharacterAbilities>().GetAbilityModifier(Ability.Constitution);
            }
             
            maxHealtHPoints = maxHealtHPoints + (abilityModifier * baseStats.GetLevel());
            return maxHealtHPoints;

        }

        public void TakeDamage(float damage, GameObject instigator)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (damage > 0)
            {
                WriteDamageToConsole(damage);
            }
            if (healthUpdated != null)
            {
                healthUpdated();
            }

            if (healthPoints <= 0)
            {
                AwardExperience();
                Die();

            }
            else if (instigator.tag == "Player")
            {
                takeDamage.Invoke(damage);
            }
        }



        public void Heal(float healing)
        {
            healthPoints = Mathf.Min(healthPoints + healing, GetMaxHealthPoints());
            if (healthUpdated != null)
            {
                healthUpdated();
            }
        }

        private void AwardExperience()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Experience experience = player.GetComponent<Experience>();
            if (experience != null && !isDead)
            {
                float experienceGained = gameObject.GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
                experience.GainExperience(experienceGained);
            }
        }



        public void Die()
        {
            if (isDead) return;
            onDie.Invoke();
            Animator animator = GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("die");
            }

            ResizeCapsuleColliderOnDeath();

            isDead = true;
            WriteDeathToConsole();
            ActionScheduler actionScheduler = GetComponent<ActionScheduler>();
            if (actionScheduler != null)
            {
                actionScheduler.CancelCurrentAction();
            }
        }

        private void ResizeCapsuleColliderOnDeath()
        {
            CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            if (capsuleCollider != null)
            {
                capsuleCollider.height = capsuleCollider.height / 10f;
                capsuleCollider.center = capsuleCollider.center / 4f;
            }
        }

        private void WriteDamageToConsole(float damage)
        {
            if (gameConsole == null) return;
                gameConsole.AddNewLine(characterName + " takes " + damage.ToString() + " damage.");
        }



        private void WriteDeathToConsole()
        {
            if (gameConsole == null) return;
            gameConsole.AddNewLine(characterName + " Death!");
        }

        private void BaseStats_onLevelUp()
        {
            float newHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            healthPoints = newHealthPoints;
            if (healthUpdated != null)
            {
                healthUpdated();
            }
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints <= 0)
            {
                onDie.Invoke();
                Die();
            }
        }
    }
}
