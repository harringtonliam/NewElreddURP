using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI.Dragging;
using RPG.InventoryControl;
using RPG.Combat;
using RPG.Control;


namespace RPG.UI.InventoryControl
{
    public class AmmunitionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;

        // CACHE
        AmmunitionStore store;

        // LIFECYCLE METHODS
        void OnDisable()
        {
            try
            {
                store.storeUpdated -= UpdateIcon;
            }
            catch
            {
                Debug.Log("AmmunitionSlotUI unable to -= storeUpdated");
            }
        }

        void OnEnable()
        {
            var player = PlayerController.GetFirstSelectedPlayer();
            store = player.GetComponent<AmmunitionStore>();
            store.storeUpdated += UpdateIcon;
            UpdateIcon();
        }

        // PUBLIC

        public void AddItems(InventoryItem item, int number)
        {
            store.AddAction(item, index, number);
        }

        public InventoryItem GetItem()
        {
            return store.GetAction(index);
        }

        public int GetNumber()
        {
            return store.GetNumber(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            store.RemoveItems(index, number);
        }

        // PRIVATE

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }
}

