using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI
{


    public class ConsoleLineUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI consoleLineText;

        public void SetText(string newText)
        {
            consoleLineText.text = newText;
        }

    }


}


