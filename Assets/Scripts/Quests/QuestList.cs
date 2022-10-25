using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;



namespace RPG.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
    {
        List<QuestStatus> statuses;

        GameConsole gameConsole;

        public event Action onListUpdated;

        private void Start()
        {
            if (statuses == null)
            {
                statuses = new List<QuestStatus>();
            }

            gameConsole = FindObjectOfType<GameConsole>();
           
        }

        public IEnumerable<QuestStatus> GetStatuses()
        {
            return statuses;
        }

        public IEnumerable<QuestStatus> GetCompleted()
        {
            List<QuestStatus> completedQuests = new List<QuestStatus>();

            foreach (var questStatus in statuses)
            {
                if (questStatus.GetCompletedCount() == questStatus.GetQuest().GetObjectiveCount())
                {
                    completedQuests.Add(questStatus);
                }
            }

            return completedQuests;
        }

        public IEnumerable<QuestStatus> GetActive()
        {
            List<QuestStatus> activeQuests = new List<QuestStatus>();

            if (statuses == null) return activeQuests;


            foreach (var questStatus in statuses)
            {
                if (questStatus.GetCompletedCount() < questStatus.GetQuest().GetObjectiveCount())
                {
                    activeQuests.Add(questStatus);
                }
            }

            return activeQuests;
        }

        public void AddQuest(Quest quest)
        {
            QuestStatus newStatus = new QuestStatus(quest);
            statuses.Add(newStatus);
            if (onListUpdated != null)
            {
                onListUpdated();
            }

            WriteToConsole("New Mission Assigned: " + quest.QuestTitle);
        }

        public void CompleteQuestObjective(Quest quest, string questObjective)
        {
            QuestStatus status = GetQuestStatus(quest);
            if (status == null) return;

            status.AddCompletedObjective(questObjective);
            if (onListUpdated != null)
            {
                onListUpdated();
            }
        }

        public bool? Evaluate(PredicateType predicate, string[] parameters)
        {

            if (statuses == null) return false;
            switch (predicate)
            {
                case PredicateType.HasQuest:
                    return HasQuest(parameters[0]);
                case PredicateType.CompletedQuest:
                    return CompeletedQuest(parameters[0]);
            }

            return null;
        }

        private bool CompeletedQuest(string questName)
        {
            foreach (var status in statuses)
            {
                if (status.GetQuest().name == questName  && status.IsCompeleted())
                {
                    return true;
                }
            }
            return false;
        }

        private void WriteToConsole(string textToWrite)
        {
            if (gameConsole == null) return;
            gameConsole.AddNewLine(textToWrite);

        }

        private bool HasQuest(string  questName)
        {
            foreach (var status in statuses)
            {
                if (status.GetQuest().name == questName)
                {
                    return true;
                }
            }
            return false;
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (var status in statuses)
            {
                if (status.GetQuest() == quest)
                {
                    return status;
                }
            }

            return null;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach (QuestStatus status in statuses)
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null) return;

            if (statuses != null)
            {
                statuses.Clear();
            }
            else
            {
                statuses = new List<QuestStatus>();
            }


            foreach (var objectState in stateList)
            {
                statuses.Add(new QuestStatus(objectState));
            }


        }


    }
}

