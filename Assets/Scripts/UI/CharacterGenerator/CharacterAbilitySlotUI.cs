using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI.Dragging;
using RPG.CharacterGenerator;
using RPG.Stats;

namespace RPG.UI.CharacterGenerator
{
    public class CharacterAbilitySlotUI : MonoBehaviour, IAbilityScoreHolder, IDragContainer<AbilityScore>
    {

        [SerializeField] AbilityScoreValueUI abilityScoreValueUIPrefab;
        [SerializeField] int index;


        CharacterAbilities characterAbilities;

        private void Awake()
        {
            characterAbilities = FindObjectOfType<CharacterAbilities>();

        }

        public void SetUp(int index, int abiliyScoreValue)
        {
            this.index = index;
            abilityScoreValueUIPrefab.SetValue(abiliyScoreValue);

        }



        public void AddItems(AbilityScore abilityScore, int number)
        {
            characterAbilities.UpdateCharacterAbility(index, abilityScore.AbilityScoreValue);
        }

        public AbilityScore GetAbilityScore()
        {
            AbilityScore abilityScore = new AbilityScore();
            abilityScore.AbilityScoreValue =  characterAbilities.GetCharacterAbilities()[index].AbilityValue;
            return abilityScore;
        }

        public AbilityScore GetItem()
        {
            AbilityScore abilityScore = new AbilityScore();
            abilityScore.AbilityScoreValue = characterAbilities.GetCharacterAbilities()[index].AbilityValue;
            return abilityScore;
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
            characterAbilities.UpdateCharacterAbility(index, 0);
        }


    }


}


