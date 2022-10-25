using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Core
{
    public class GameConsole : MonoBehaviour
    {
        [SerializeField] int maxLineCount = 10;

        List<string> consoleLines;

        public event Action onLineAdded;

        public int MaxLineCount
        {
            get { return maxLineCount; }
        }

        private void Awake()
        {
            consoleLines = new List<string>();
        }

        public void AddNewLine (string newLine)
        {
            consoleLines.Add(newLine);
            RemoveLine();
            if (onLineAdded != null)
            {
                onLineAdded();
            }

        }

        public string GetLastLine()
        {
            if (consoleLines.Count > 0)
            {
                return consoleLines.Last();
            }
            else
            {
                return string.Empty;
            }
            
        }

        public List<string> GetAllLines()
        {
            return consoleLines;
        }

        private void RemoveLine()
        {
            if (consoleLines.Count>maxLineCount)
            {
                try
                {
                    consoleLines.RemoveAt(0);
                }
                catch (Exception)
                {

                    Debug.Log("GameConsole - failed ot remove first time");
                }

            }

        }

        



    }


}


