using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Quests;
using TMPro;
using UnityEngine.UI;
using System;

namespace RPG.UI.Quests
{
    public class QuestsUI : MonoBehaviour
    {
        [SerializeField] GameObject questStatusPrefab;
        [SerializeField] Transform questListRoot;
        [SerializeField] TextMeshProUGUI questTitle;
        [SerializeField] TextMeshProUGUI questDetails;
        [SerializeField] TextMeshProUGUI questsShown;
        [SerializeField] Button showCompletedButton;
        [SerializeField] GameObject completedStatusUI;

        QuestList questList;
        bool showCompleted = false;

        // Start is called before the first frame update
        void Start()
        {
            questList = FindObjectOfType<QuestList>();
            BuildQuestList(); 
            SetHeadings();
            questList.onListUpdated += BuildQuestList;
            showCompletedButton.onClick.AddListener(ShowCompleted);
        }

        private void ShowCompleted()
        {
            showCompleted = !showCompleted;

            SetHeadings();

            BuildQuestList();
        }

        private void SetHeadings()
        {
            if (showCompleted)
            {
                questsShown.SetText("Completed Missions");
                showCompletedButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Show Active Missions");
            }
            else
            {
                questsShown.SetText("Active Missions");
                showCompletedButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Show Completed Missions");
            }
        }

        private void BuildQuestList()
        {
            foreach (Transform item in questListRoot)
            {
                Destroy(item.gameObject);
            }

            if (questList.GetStatuses() == null) return;
            IEnumerable<QuestStatus> questsToDisplay;
            if (showCompleted)
            {
                 questsToDisplay =  questList.GetCompleted();
            }
            else
            {
                 questsToDisplay = questList.GetActive();
            }

            foreach (QuestStatus questStatus in questsToDisplay)
            {
                GameObject questListButton = GameObject.Instantiate(questStatusPrefab, questListRoot);
                var textComp = questListButton.GetComponentInChildren<TextMeshProUGUI>();
                textComp.SetText(questStatus.GetQuest().QuestTitle);
                Button button = questListButton.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => {
                    DisplayQuestDetails(questStatus);
                });

            }
        }

        private void DisplayQuestDetails(QuestStatus questStatus )
        {
            questTitle.SetText(questStatus.GetQuest().QuestTitle);
            questDetails.SetText(questStatus.GetQuest().QuestDescription);
            completedStatusUI.SetActive(questStatus.IsCompeleted());
        }
    }

}


