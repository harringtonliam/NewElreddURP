using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName ="Stats/New Progession", order = 0)]
    public class Progression : ScriptableObject
    {

        [SerializeField] ProgressionCharacterClass[] progressionCharacterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;


        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookUp();

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length < level) 
            {
                return 0;
            }

            return levels[level-1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookUp();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        public void BuildLookUp()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (var progressionsCharacterClass in progressionCharacterClasses)
            {
                var statLookUpTable = new Dictionary<Stat, float[]>();

                foreach (var progressionstat in progressionsCharacterClass.stats)
                {
                    statLookUpTable[progressionstat.stat] = progressionstat.levels;
                }

                lookupTable[progressionsCharacterClass.characterClass] = statLookUpTable;
            }

        }

        [System.Serializable]
        class ProgessionStat
        {
            public Stat stat;
            public float[] levels;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgessionStat[] stats;
        }


    }
}
