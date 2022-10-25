using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using TMPro;
using UnityEngine.UI;
using System;

namespace RPG.UI
{


    public class ConsoleUI : MonoBehaviour
    {
        [SerializeField] GameObject consoleLinePrefab;
        [SerializeField] ScrollRect scrollRect;


        GameConsole console;

        // Start is called before the first frame update
        void Start()
        {
            console = FindObjectOfType<GameConsole>();
            console.onLineAdded += UpdateGUI;

            ResetGUI();
        }



        private void OnDestroy()
        {
            if (console != null)
            {
                console.onLineAdded -= UpdateGUI;
            }

        }

        private void UpdateGUI()
        {
            GameObject consoleLineUI = Instantiate(consoleLinePrefab, transform);
            CheckNumberOfLines();
            string newText = console.GetLastLine();
            consoleLineUI.GetComponent<ConsoleLineUI>().SetText(newText);

            ScrollToLastLIne();
        }



        private void CheckNumberOfLines()
        {
            if(transform.childCount > console.MaxLineCount)
            {
                int numberOfLinesToRemove = transform.childCount - console.MaxLineCount;
                for (int i = 0; i < numberOfLinesToRemove; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
              
            }
        }

        private void ResetGUI()
        {
            foreach (string consoleLine in console.GetAllLines())
            {
                GameObject consoleLineUI = Instantiate(consoleLinePrefab, transform);
                consoleLineUI.GetComponent<ConsoleLineUI>().SetText(consoleLine);
            }
            ScrollToLastLIne();
        }

        private void ScrollToLastLIne()
        {
            scrollRect.verticalNormalizedPosition = 0f;
            Canvas.ForceUpdateCanvases();
        }
    }
}





