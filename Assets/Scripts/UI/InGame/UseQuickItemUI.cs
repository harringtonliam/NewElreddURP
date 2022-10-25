using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.UI.InventoryControl;

namespace RPG.UI.InGame
{
    public class UseQuickItemUI : MonoBehaviour
    {
        [SerializeField] int index;

        [SerializeField] InventoryItemIcon icon = null;


        GameObject player;
        QuickItemStore quickItemStore;


        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            quickItemStore = player.GetComponent<QuickItemStore>();
            quickItemStore.storeUpdated += UpdateIcon;
        }

        public ActionItem GetItem()
        {
            return quickItemStore.GetAction(index);
        }

        public int GetNumber()
        {
            return quickItemStore.GetNumber(index);
        }

        public void UseItem()
        {
            ActionItem actionItem = quickItemStore.GetAction(index);
            if (actionItem != null)
            {
                quickItemStore.Use(index, player);
            }

        }

        private void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }

}


