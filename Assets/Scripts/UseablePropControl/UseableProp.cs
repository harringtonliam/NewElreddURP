using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPG.Control;
using RPG.Core;

namespace RPG.UseablePropControl
{

    public class UseableProp : MonoBehaviour, IRaycastable
    {
        [SerializeField] bool isActivated = false;
        [SerializeField] UnityEvent<bool> useProp;
        [SerializeField] UnityEvent<float> usePropFloat;
        [SerializeField] UnityEvent<PatrolPath> usePropPatrolPath;
        [SerializeField] UnityEvent<GameObject> usePropCombatTarget;
        [TextArea]
        [SerializeField] string displayText = "This thing does stuff";
        [TextArea]
        [SerializeField] string activateText = "Click to activate this item";
        [TextArea]
        [SerializeField] string deactivateText = "Click to deactivate this item";
        [TextArea]
        [SerializeField] string onActivatedText = "Item activated";
        [TextArea]
        [SerializeField] string onDeactivatedText = "Item deactivated";
        [SerializeField] float deactivatedValue = 5f;
        [SerializeField] float activatedValue = 10f;
        [SerializeField] PatrolPath deactivatedPatrolPath;
        [SerializeField] PatrolPath activatedPatrolPath;
        [SerializeField] GameObject deactivatedCombatTarget;
        [SerializeField] GameObject activatedCombatTaregt;
        [SerializeField] AudioSource activateSound;
        [SerializeField] AudioSource deactivateSound;

        public string DisplayText
        {
            get { return displayText; }
        }

        public string ActivateText
        {
            get { return activateText; }
        }

        public string DeactivateText
        {
            get { return deactivateText; }
        }


        UseablePropLink useablePropLink;
        GameConsole gameConsole;

        private void Start()
        {
            useablePropLink = FindObjectOfType<UseablePropLink>();
            gameConsole = FindObjectOfType<GameConsole>();
        }

        public CursorType GetCursorType()
        {
            return CursorType.Use;
        }

        public RaycastableReturnValue HandleRaycast(PlayerController playerController)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    UseProp useProp = playerController.transform.GetComponent<UseProp>();
            //    if (useProp != null)
            //    {
            //        useProp.StartUseProp(gameObject);
            //    }
            //}
            return RaycastableReturnValue.FirstPlayerCharacter;
        }

        public void HandleActivation(PlayerController playerController)
        {
            UseProp useProp = playerController.transform.GetComponent<UseProp>();
            if (useProp != null)
            {
                useProp.StartUseProp(gameObject);
            }
        }


        public void UseProp()
        {
            if(useablePropLink != null)
            {
                useablePropLink.DisplayUsePropUI(this);
            }
        }

        public void SetPropActivatedStatus(bool activatedStatus)
        {
            isActivated = activatedStatus;
            useProp.Invoke(isActivated);
            if (isActivated)
            {
                usePropFloat.Invoke(activatedValue);
                usePropPatrolPath.Invoke(activatedPatrolPath);
                usePropCombatTarget.Invoke(activatedCombatTaregt);
                WriteToConsole(onActivatedText);
            }
            else
            {
                usePropFloat.Invoke(deactivatedValue);
                usePropPatrolPath.Invoke(deactivatedPatrolPath);
                usePropCombatTarget.Invoke(deactivatedCombatTarget);
                WriteToConsole(onDeactivatedText);
            }

            if (isActivated && activateSound != null)
            {
                activateSound.Play();
            }
            else if (!isActivated && deactivateSound != null)
            {
                deactivateSound.Play();
            }

        }

        private void WriteToConsole(string textToWrite)
        {
            if (gameConsole == null) return;

            gameConsole.AddNewLine(textToWrite.Replace("\n", " "));
        }




    }
}

