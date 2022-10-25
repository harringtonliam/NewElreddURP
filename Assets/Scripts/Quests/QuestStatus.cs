using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [System.Serializable]
    public class QuestStatus 
    {
        [SerializeField] Quest quest;
        [SerializeField] List<string> completedObjectives;

        [System.Serializable]
        class QuestStatusRecord
        {
            public string questName;
            public  List<string> completedObjectives;

        }

        public QuestStatus(Quest quest)
        {
            this.quest = quest;
            completedObjectives = new List<string>();
        }

        public QuestStatus(object objectState)
        {
            QuestStatusRecord state = objectState as QuestStatusRecord;
            quest = Quest.GetByName(state.questName);
            completedObjectives = state.completedObjectives;
        }

        public Quest GetQuest()
        {
            return quest;
        }

        public int GetCompletedCount()
        {
            if (completedObjectives == null) return 0;
            return completedObjectives.Count;
        }

        public bool IsCompeleted()
        {
            if (completedObjectives == null) return false;
            if (completedObjectives.Count == GetQuest().GetObjectiveCount())
            {
                return true;
            }
            return false;
        }

        public void AddCompletedObjective(string completedObjective)
        {
            completedObjectives.Add(completedObjective);
            if (IsCompeleted())
            {
                AwardExperience();
            }
        }

        private void AwardExperience()
        {
            GameObject player = GameObject.FindWithTag("Player");
            Experience experience = player.GetComponent<Experience>();
            if (player.GetComponent<Experience>() != null)
            {
                float experienceGained = GetQuest().ExperiancePointReward;
                experience.GainExperience(experienceGained);
            }
        }

        public object CaptureState()
        {
            QuestStatusRecord questStatusRecord = new QuestStatusRecord();
            questStatusRecord.questName = quest.name;
            questStatusRecord.completedObjectives = completedObjectives;
            return questStatusRecord;
        }
    }

}


