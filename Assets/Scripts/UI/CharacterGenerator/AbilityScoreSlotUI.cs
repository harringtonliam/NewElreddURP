using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CharacterGenerator;
using RPG.UI.Dragging;
using System;

namespace RPG.UI.CharacterGenerator
{

    public class AbilityScoreSlotUI : MonoBehaviour, IDragContainer<AbilityScore>
    {
        [SerializeField] AbilityScoreValueUI abilityScoreValueUIPrefab;
        [SerializeField] int index;



        RolledScores rolledScores;



        public void SetUp(RolledScores rolledScores, int abiliyScoreValue, int index)
        {
             this.rolledScores = rolledScores;
            this.index = index;
            abilityScoreValueUIPrefab.SetValue(abiliyScoreValue);
        }


        public void AddItems(AbilityScore abilityScore, int number)
        {
            rolledScores.UpdateScore(index, abilityScore);
        }

        public AbilityScore GetItem()
        {
            Debug.Log("AbilityScoreSlotUI GetItem" + index.ToString());
            return rolledScores.GetAbilityScores()[index];
        }

        public int GetNumber()
        {
            return 1;
        }

        public int MaxAcceptable(AbilityScore item)
        {
            return 1;
        }



        public void RemoveItems(int number)
        {
            rolledScores.RemoveFromSlot(index);
        }
    }

}
