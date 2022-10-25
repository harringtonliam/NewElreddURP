using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Stats;

namespace RPG.UI.Information
{

    public class CharacterClassUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI characterClassNameText;
        [SerializeField] TextMeshProUGUI characterLevelText;
        [SerializeField] TextMeshProUGUI characterXPText;
        [SerializeField] TextMeshProUGUI characterXPForNextLevelText;

        BaseStats baseStats = null;
        GameObject player = null;
        Experience experience;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            baseStats = player.GetComponent<BaseStats>();
            experience = player.GetComponent<Experience>();

            experience.onExperiencedGained += RedrawUI;

            RedrawUI();
        }



        private void RedrawUI()
        {
            characterClassNameText.text = baseStats.CharacterClass.ToString();
            characterLevelText.text = baseStats.GetLevel().ToString();
            characterXPText.text = experience.ExperiencePoints.ToString();
            characterXPForNextLevelText.text = baseStats.Progression.GetStat(Stat.ExperienceToLevelUp, baseStats.CharacterClass, baseStats.GetLevel()+1).ToString();

        }

    }


}


