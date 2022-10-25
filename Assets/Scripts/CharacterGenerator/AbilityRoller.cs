using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;

namespace RPG.CharacterGenerator
{
    public class AbilityRoller : MonoBehaviour
    {


        public int RollAbility()
        {

            Dice dice = FindObjectOfType<Dice>();
            int[] diceRolls = new int[4];
            int abilityScore = 0;
            
            for (int i = 0; i < diceRolls.Length; i++)
            {
                diceRolls[i] = dice.RollDice(6, 1);
            }

            Array.Sort(diceRolls);
            Array.Reverse(diceRolls);
            for (int i = 0; i < 3; i++)
            {
                abilityScore += diceRolls[i];
            }

            return abilityScore;
        }

    }
}


