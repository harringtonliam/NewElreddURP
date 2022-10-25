using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.CharacterGenerator
{
    public class RolledScores : MonoBehaviour
    {

        [SerializeField] int numberOfScoresToRoll = 6;

        [SerializeField] AbilityScore[] abilityScores;

        public event Action rolledScoresUpdated;


        public AbilityScore[] GetAbilityScores()
        {
            return abilityScores;
        }

        // Start is called before the first frame update
        void Start()
        {
            abilityScores = new AbilityScore[numberOfScoresToRoll];

        }

        public void RollScores()
        {
            AbilityRoller abilityRoller = FindObjectOfType<AbilityRoller>();
            for (int i = 0; i < abilityScores.Length; i++)
            {
                AbilityScore abilityScore = new AbilityScore();
                abilityScore.AbilityScoreValue = abilityRoller.RollAbility();
                abilityScores[i] = abilityScore;
            }
            if (rolledScoresUpdated != null)
            {
                rolledScoresUpdated();
            }


        }

        public void UpdateScore(int index, AbilityScore newabilityScore)
        {
            abilityScores[index] = newabilityScore;
            if (rolledScoresUpdated != null)
            {
                rolledScoresUpdated();
            }
        }

        public void RemoveFromSlot(int index)
        {
            abilityScores[index] = null;
        }


    }
}


