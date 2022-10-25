using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Core;
using RPG.Attributes;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 5)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpPrefab = null;

        public event Action onLevelUp;

        int currentLevel = 0;
        Experience experience;
        GameConsole gameConsole;
        CharacterSheet characterSheet;

        public CharacterClass CharacterClass
        { 
            get { return characterClass;}
        }

        public Progression Progression
        {
            get { return progression; }
        }


        private void Awake()
        {
            experience = GetComponent<Experience>();
        }

        private void Start()
        {
            currentLevel = CalculateLevel();
            gameConsole = FindObjectOfType<GameConsole>();
            characterSheet = GetComponent<CharacterSheet>();
        }

        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperiencedGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperiencedGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();

            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            if (levelUpPrefab != null)
            {
                Instantiate(levelUpPrefab, transform);
            }
            WriteToConsole("level up.  New level = " + currentLevel.ToString());
        }

        public float GetStat(Stat stat)
        {

            int level = GetLevel();

            return (progression.GetStat(stat, characterClass, level));
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float statModifier = 0f;

            IModiifierProvider[] modiifierProviders = GetComponents<IModiifierProvider>();
            foreach (var modifierProvider in modiifierProviders)
            {
                foreach (var modifier in modifierProvider.GetAdditiveModifiers(stat))
                {
                    statModifier += modifier;
                }
            }

            return statModifier;
        }

        private float GetPercenatgeModifier(Stat stat)
        {
            float statModifier = 0f;

            IModiifierProvider[] modiifierProviders = GetComponents<IModiifierProvider>();
            foreach (var modifierProvider in modiifierProviders)
            {
                foreach (var modifier in modifierProvider.GetPercentageModifiers(stat))
                {
                    statModifier += modifier;
                }
            }

            return statModifier;
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null)
            {
                return startingLevel;
            }


            float currentXP = GetComponent<Experience>().ExperiencePoints;
            int maxLevels  = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = maxLevels; level >= 1; --level)
            {
                if(currentXP >= progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level))
                {
                    return level;
                }
            }
            return 1;

        }

        private void WriteToConsole(string textToWrite)
        {
            if (gameConsole == null) return;
            string name = string.Empty;
            if (characterSheet != null)
            {
                name = characterSheet.CharacterName;
            }
            gameConsole.AddNewLine(name + ": " + textToWrite);
        }



    }

}
