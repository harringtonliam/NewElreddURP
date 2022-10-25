using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Stats;


namespace RPG.UI.Information
{



    public class ClassDetailsUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI maxHitPointsText;
        [SerializeField] TextMeshProUGUI baseAttackBonusText;

        BaseStats baseStats = null;
        GameObject player = null;
        Experience experience;
        CharacterAbilities characterAbilities;


        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            baseStats = player.GetComponent<BaseStats>();
            experience = player.GetComponent<Experience>();
            characterAbilities = player.GetComponent<CharacterAbilities>();

            experience.onExperiencedGained += RedrawUI;

            RedrawUI();
        }



        private void RedrawUI()
        {
            float maxHitPoints = baseStats.Progression.GetStat(Stat.Health, baseStats.CharacterClass, baseStats.GetLevel());
            maxHitPoints = maxHitPoints + characterAbilities.GetAbilityModifier(Ability.Constitution);
            maxHitPointsText.text = maxHitPoints.ToString();
            float attackBonus = baseStats.Progression.GetStat(Stat.BaseAttackBonus, baseStats.CharacterClass, baseStats.GetLevel());
            if (attackBonus > 0)
            {
                baseAttackBonusText.text = "+" + attackBonus.ToString();
            }
            else
            {
                baseAttackBonusText.text = "-" + attackBonus.ToString();
            }
            
        }
    }

}
