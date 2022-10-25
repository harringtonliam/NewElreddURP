using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.CharacterGenerator
{
    [Serializable]
    public class AbilityScore
    {
        [SerializeField] int abilityScoreValue;

        public int AbilityScoreValue
        {
            get { return abilityScoreValue; }
            set { abilityScoreValue = value; }
        }


    }
}


