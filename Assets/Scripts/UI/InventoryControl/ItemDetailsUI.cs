using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RPG.InventoryControl;


namespace RPG.UI.InventoryControl
{
    public class ItemDetailsUI : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            var item = GetComponent<IItemHolder>().GetItem();
            if (!item) return;

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                DisplayDetailsUI(item);
            }

        }

        private void DisplayDetailsUI(InventoryItem item)
        {
            InventoryItemDetailsUI inventoryItemDetailsUI = GetComponentInParent<InventoryItemDetailsUI>();
            if (inventoryItemDetailsUI == null) return;
            inventoryItemDetailsUI.gameObject.SetActive(true);
            inventoryItemDetailsUI.Setup(item);



        }
    }
}


