using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{

    [CreateAssetMenu(fileName = "AbilityModifiers", menuName = "Stats/New AbilityModifiers", order = 0)]
    public class AbilityModifiers : ScriptableObject
    {
        [SerializeField] int[] modifiers = null;

        public int GetModifier(int abilityValue)
        {
            if (abilityValue > modifiers.Length -1)
            {
                return 0;
            }
            else
            {
                return modifiers[abilityValue - 1];
            }

        }
    }

   

}
