using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CharacterGenerator;

namespace RPG.UI.CharacterGenerator
{

    public class AbilityScoresUI : MonoBehaviour
    {
        [SerializeField] AbilityScoreSlotUI abilityScoreSlotPrefab = null;
        [SerializeField] Transform rolledScoreContainer;

        RolledScores rolledScores;
  



        private void Start()
        {
            rolledScores = FindObjectOfType<RolledScores>();
            rolledScores.rolledScoresUpdated += Redraw;

        }

        public void RollAbilityScores()
        {
            rolledScores.RollScores();
        }


        public void Redraw()
        {
            foreach (Transform child in rolledScoreContainer)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < rolledScores.GetAbilityScores().Length; i++)
            {
                if (rolledScores.GetAbilityScores()[i] != null)
                {
                    var rolledScoreUI = Instantiate(abilityScoreSlotPrefab, rolledScoreContainer);

                    rolledScoreUI.SetUp(rolledScores, rolledScores.GetAbilityScores()[i].AbilityScoreValue, i);
                }

            }
        }

    }

}

