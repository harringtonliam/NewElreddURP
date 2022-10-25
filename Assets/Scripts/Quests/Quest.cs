using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "RPG/Quest", order = 1)]
    public class Quest : ScriptableObject
    {
        [SerializeField] string questTitle;
        [TextArea]
        [SerializeField] string questDescription;
        [SerializeField] int experiancePointReward;
        [SerializeField] string[] objectives;
        

        public string QuestTitle
        {
            get { return questTitle; }
        }

        public string QuestDescription
        {
            get { return questDescription; }
        }

        public int ExperiancePointReward
        {
            get { return experiancePointReward; }
        }

        public int GetObjectiveCount()
        {
            if (objectives == null) return 0;
            return objectives.Length;
        }

        public IEnumerable<string> GetObjectives()
        {
            return objectives;
        }

        public static Quest GetByName(string questName)
        {
            foreach (Quest quest in Resources.LoadAll<Quest>(""))
            {
                if (quest.name == questName)
                {
                    return quest;
                }
            }

            return null;

        }

    }

}


