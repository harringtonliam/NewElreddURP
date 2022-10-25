using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using UnityEngine.UI;
using TMPro;
using RPG.Attributes;

namespace RPG.UI.InventoryControl
{
    public class OpenContainerUI : MonoBehaviour
    {
        [SerializeField] GameObject uiCanvas = null;
        [SerializeField] InventoryUI containerInventoryUI = null;
        [SerializeField] ScrollRect containerscrollRect;
        [SerializeField] ScrollRect playerscrollRect;
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] TextMeshProUGUI containerName;
        [SerializeField] Image playerImage;
        [SerializeField] Image containerImage;
        [SerializeField] string defaultContainerName;
        [SerializeField] Sprite defaultContainerImage;

        void Start()
        {
            uiCanvas.SetActive(false);
        }

        public void OpenContainer(Inventory inventory)
        {
            uiCanvas.SetActive(true);
            if (containerInventoryUI != null)
            {
                containerInventoryUI.SetInventoryObject(inventory);
            }

            if (containerscrollRect != null)
            {
                containerscrollRect.verticalNormalizedPosition = 1f;
                Canvas.ForceUpdateCanvases();
            }
            if (playerscrollRect != null)
            {
                playerscrollRect.verticalNormalizedPosition = 1f;
                Canvas.ForceUpdateCanvases();
            }

            CharacterSheet containerCharacterSheet = inventory.GetComponent<CharacterSheet>();
            if (containerCharacterSheet != null)
            {
                containerName.text = containerCharacterSheet.CharacterName;
                containerImage.sprite = containerCharacterSheet.Portrait;
            }
            else
            {
                containerName.text = defaultContainerName;
                containerImage.sprite = defaultContainerImage;
            }
            CharacterSheet characterSheet = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterSheet>();
            playerName.text = characterSheet.CharacterName;
            playerImage.sprite = characterSheet.Portrait;


        }

        public void CloseContainer()
        {
            uiCanvas.SetActive(false);
        }

    }

}


