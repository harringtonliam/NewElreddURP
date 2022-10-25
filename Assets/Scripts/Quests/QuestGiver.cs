using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{

    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;

        public void GiveQuest()
        {
            QuestList questList = FindObjectOfType<QuestList>();
            if (questList == null) return;
            questList.AddQuest(quest);
        }
        
    }
}


