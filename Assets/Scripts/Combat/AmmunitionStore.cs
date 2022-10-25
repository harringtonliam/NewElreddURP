using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.InventoryControl;
using System;

namespace RPG.Combat
{
    public class AmmunitionStore : MonoBehaviour, ISaveable
    {
        [SerializeField]
        DockedItemSlot[] dockedItems = new DockedItemSlot[3];

        [Serializable]
        private class DockedItemSlot
        {
            public Ammunition ammunition;
            public int number;
        }

        public event Action storeUpdated;


        public Ammunition GetAction(int index)
        {
            if (dockedItems[index] != null)
            {
                return dockedItems[index].ammunition;
            }
            return null;

        }

        public int GetNumber(int index)
        {
            if (dockedItems.Length > index && dockedItems[index] != null)
            {
                return dockedItems[index].number;
            }
            return 0;
        }


        public void AddAction(InventoryItem item, int index, int number)
        {
            if (object.ReferenceEquals(item, dockedItems[index].ammunition))
            {
                dockedItems[index].number += number;
            }
            else
            {
                var slot = new DockedItemSlot();
                slot.ammunition = item as Ammunition;
                slot.number = number;
                dockedItems[index] = slot;
            }
            if (storeUpdated != null)
            {
                storeUpdated();
            }
        }

        public void RemoveItems(int index, int number)
        {
            if (dockedItems.Length > index)
            {
                dockedItems[index].number -= number;
                if (dockedItems[index].number <= 0)
                {
                    dockedItems[index].ammunition = null;
                    dockedItems[index].number = 0;
                }
                if (storeUpdated != null)
                {
                    storeUpdated();
                }
            }
        }

        public int MaxAcceptable(InventoryItem item, int index)
        {
            var actionItem = item as Ammunition;
            if (!actionItem) return 0;

            if (dockedItems.Length <= index && !object.ReferenceEquals(item, dockedItems[index].ammunition))
            {
                return 0;
            }
            if (actionItem.IsStackable)
            {
                return item.MaxNumberInStack;
            }
            if (dockedItems.Length <= index)
            {
                return 0;
            }

            return 1;
        }

        public int FindAmmunitionType(AmmunitionType ammunitionType)
        {
            for (int i = 0; i < dockedItems.Length; i++)
            {
                if (dockedItems[i].ammunition != null && dockedItems[i].ammunition.AmmunitionType == ammunitionType)
                {
                    return i;
                }
            }
            return -1;
        }

        [System.Serializable]
        private struct DockedItemRecord
        {
            public string itemID;
            public int number;
        }

        public object CaptureState()
        {
            var state = new DockedItemRecord[3];
            for (int i = 0; i < dockedItems.Length; i++)
            {
                if (dockedItems[i].ammunition != null)
                {
                    state[i].itemID = dockedItems[i].ammunition.ItemID;
                    state[i].number = dockedItems[i].number;
                 }
            }
            return state;

        }

        public void RestoreState(object state)
        {
            var stateDict = (DockedItemRecord[])state;

            for (int i = 0; i < stateDict.Length; i++)
            {
                AddAction(Ammunition.GetFromID(stateDict[i].itemID) as Ammunition, i, stateDict[i].number);
            }

            if (storeUpdated != null)
            {
                storeUpdated();
            }

        }

    }
}




