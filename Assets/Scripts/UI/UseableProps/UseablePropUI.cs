using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UseablePropControl;
using TMPro;
using UnityEngine.UI;
using System;

namespace RPG.UI.UseableProps
{
    public class UseablePropUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI displayText;
        [SerializeField] TextMeshProUGUI activateButtonText;
        [SerializeField] TextMeshProUGUI deaactivateButtonText;
        [SerializeField] Button closeButton;
        [SerializeField] Button activateButton;
        [SerializeField] Button deactivateButton;
        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] GameObject actionCanvas = null;

        UseablePropLink usablePropLink;
        GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            usablePropLink = FindObjectOfType<UseablePropLink>();
            usablePropLink.onDisplayUseablePropUI += ShowDisplay;
            closeButton.onClick.AddListener(Close);
            activateButton.onClick.AddListener(ActivateButtonClick);
            deactivateButton.onClick.AddListener(DectivateButtonClick);

            player = GameObject.FindGameObjectWithTag("Player");
            
        }

        private void ShowDisplay()
        {
            if (usablePropLink == null) return;

            displayText.text = usablePropLink.CurrentUsableProp.DisplayText;
            activateButtonText.text = usablePropLink.CurrentUsableProp.ActivateText;
            deaactivateButtonText.text = usablePropLink.CurrentUsableProp.DeactivateText;
            player.GetComponent<UseProp>().onUsePropCancel += HideDisplay;

            if (usablePropLink.CurrentUsableProp.ActivateText == string.Empty)
            {
                actionCanvas.SetActive(false);
            }
            else
            {
                actionCanvas.SetActive(true);
            }

            uiCanvas.SetActive(true);
        }

        private void ActivateButtonClick()
        {
            if (usablePropLink.CurrentUsableProp != null)
            {
                usablePropLink.CurrentUsableProp.SetPropActivatedStatus(true);
              
            }
            Close();
        }

        private void DectivateButtonClick()
        {
            if (usablePropLink.CurrentUsableProp != null)
            {
                usablePropLink.CurrentUsableProp.SetPropActivatedStatus(false);
            }
            Close();
        }

        private void HideDisplay()
        {
            uiCanvas.SetActive(false);
        }

        private void Close()
        {
            player.GetComponent<UseProp>().Cancel();
            HideDisplay();
        }

        private void OnDestroy()
        {
            try
            {
                player.GetComponent<UseProp>().onUsePropCancel -= HideDisplay;
            }
            catch
            {
                Debug.Log("UseablePropUI On destroy : unable to -= onUsePropCancel");
            }
            
        }
    }

}


