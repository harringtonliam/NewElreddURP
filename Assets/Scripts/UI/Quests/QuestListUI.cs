using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI questTitle;

        public void Setup(string newQuestTitle)
        {
            questTitle.text = newQuestTitle;
        }

    }
}


