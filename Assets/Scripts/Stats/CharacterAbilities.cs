using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Stats
{
    public class CharacterAbilities : MonoBehaviour
    {
        [SerializeField] CharacterAbility[] characterAbilities;
        [SerializeField] AbilityModifiers abilityModifiers;

        public event Action characterAbilitiesUpdated;

        public int GetAbilityModifier(Ability ability)
        {
            int abilityScore = 0;

            foreach (var characterAbility in characterAbilities)
            {
                if (characterAbility.Ability == ability)
                {
                    abilityScore = characterAbility.AbilityValue;
                }
            }
            return abilityModifiers.GetModifier(abilityScore);
        }

        public CharacterAbility[] GetCharacterAbilities()
        {
            return characterAbilities;
        }

        public void UpdateCharacterAbility(int index, int abilityValue)
        {
            characterAbilities[index].AbilityValue = abilityValue;

            if (characterAbilitiesUpdated != null)
            {
                characterAbilitiesUpdated();
            }
        }
    }

    [System.Serializable]
    public class CharacterAbility
    {
        [SerializeField] Ability ability;
        [SerializeField] int abilityValue = 10;

        public Ability Ability { get { return ability; } }
        public int AbilityValue {  get { return abilityValue;  }  set { abilityValue = value; } }

    }


    
}
