using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Dice : MonoBehaviour
    {
        public int RollDice(int diceSize, int diceNumber)
        {
            int result = 0;

            for (int i = 0; i < diceNumber; i++)
            {
                int diceRoll = Random.Range(1, diceSize+1);
                result += diceRoll;
            }

            return result;
        }
    }
}
