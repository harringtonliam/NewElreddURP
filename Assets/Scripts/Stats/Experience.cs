using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Core;
using RPG.Attributes;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        CharacterSheet characterSheet;
        GameConsole gameConsole;


        private void Start()
        {
            gameConsole = FindObjectOfType<GameConsole>();
            characterSheet = GetComponent<CharacterSheet>();
        }

        public float ExperiencePoints
        {
            get { return experiencePoints; }
        }


        //public delegate void ExperianceGainedDelegate();
        public event Action onExperiencedGained; //Using Action means I don't need to the delegate defined above

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            WriteToConsole(experience);
            onExperiencedGained();
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

        private void WriteToConsole(float experience)
        {
            if (gameConsole == null) return;
            string characterName = string.Empty;
            if (characterSheet != null)
            {
                characterName = characterSheet.CharacterName;
            }
            gameConsole.AddNewLine(characterName + ": gains " + experience.ToString() + " experience points.");
        }
    }
}


