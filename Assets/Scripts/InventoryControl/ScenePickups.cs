using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.InventoryControl
{
    public class ScenePickups : MonoBehaviour, ISaveable
    { 

        List<InventoryItemSlot> currentItemsInScene = null;
        List<InventoryItemSlot> originalItemsInScene = null;


        public struct InventoryItemSlot
        {
            public GameObject pickupGameObject;
            public InventoryItem inventoryItem;
            public int number;
            public Vector3 position;
        }

        private void Awake()
        {
            if (originalItemsInScene == null)
            {
                originalItemsInScene = AddAllPickupsInScene();
            }
            if (currentItemsInScene == null)
            {
                currentItemsInScene = originalItemsInScene;
            }
           
        }

        public void AddItem(InventoryItem newItem, int number, Vector3 position )
        {
            InventoryItemSlot inventoryItemSlot;
            inventoryItemSlot.pickupGameObject = null;
            inventoryItemSlot.inventoryItem = newItem;
            inventoryItemSlot.number = number;
            inventoryItemSlot.position = position;
            currentItemsInScene.Add(inventoryItemSlot);
        }

        public void RemoveItem(InventoryItem itemToRemove, int number, Vector3 position)
        {
            foreach (var itemInScene in currentItemsInScene)
            {
                if (itemInScene.inventoryItem == itemToRemove && itemInScene.number == number && itemInScene.position == position)
                {
                    currentItemsInScene.Remove(itemInScene);
                    return;
                }
            }
        }

        private List<InventoryItemSlot> AddAllPickupsInScene()
        {
            List<InventoryItemSlot> pickupsInScene = new List<InventoryItemSlot>();
            Pickup[] allpickups = FindObjectsOfType<Pickup>();
            foreach (var pickup in allpickups)
            {
                InventoryItemSlot inventoryItemSlot;
                inventoryItemSlot.pickupGameObject = pickup.gameObject;
                inventoryItemSlot.inventoryItem = pickup.InventoryItem;
                inventoryItemSlot.number = pickup.NumberOfItems;
                inventoryItemSlot.position = pickup.transform.position;
                pickupsInScene.Add(inventoryItemSlot);
            }

            return pickupsInScene;
        }

        private void  CreatePickups()
        {
            foreach (var slot in originalItemsInScene)
            {
                Pickup spawnedPickup = slot.inventoryItem.SpawnPickup(slot.position, slot.number);
                spawnedPickup.transform.parent = this.transform;
            }
        }

        private void DestroyPickups(List<InventoryItemSlot> itemsToDestroy)
        {
            foreach (var item in itemsToDestroy)
            {
                if (item.pickupGameObject != null)
                {
                    Destroy(item.pickupGameObject);
                }
            }
        }

        [System.Serializable]
        public struct SaveableInventoryItemSlot
        {
            public string inventoryItemId;
            public int number;
            public SerializableVector3 position;
        }

        public object CaptureState()
        {
            SaveableInventoryItemSlot[] itemsToSave =  new SaveableInventoryItemSlot[currentItemsInScene.Count];
            int i = 0;
            foreach (var inventoryItemSlot in currentItemsInScene)
            {
                itemsToSave[i].inventoryItemId = inventoryItemSlot.inventoryItem.ItemID;
                itemsToSave[i].number = inventoryItemSlot.number;
                itemsToSave[i].position = new SerializableVector3(inventoryItemSlot.position);
                i++;
            }

            return itemsToSave;
        }

        public void RestoreState(object state)
        {
            if (originalItemsInScene == null)
            {
                originalItemsInScene = AddAllPickupsInScene();

            }
            DestroyPickups(originalItemsInScene);
            if (currentItemsInScene == null)
            {
                currentItemsInScene = new List<InventoryItemSlot>();
            }
            currentItemsInScene.Clear();

            var slotStrings = (SaveableInventoryItemSlot[])state;
            for (int i = 0; i < slotStrings.Length; i++)
            {
                InventoryItemSlot newSlot;
                InventoryItem newItem = InventoryItem.GetFromID(slotStrings[i].inventoryItemId);
                if (newItem != null)
                {
                    Vector3 newPosition = slotStrings[i].position.ToVector();
                    newSlot.position = newPosition;

                    Pickup newPickup = newItem.SpawnPickup(newPosition, slotStrings[i].number);
                    newPickup.transform.parent = this.transform;

                    newSlot.pickupGameObject = newPickup.gameObject;
                    newSlot.inventoryItem = newItem;
                    newSlot.number = slotStrings[i].number;
                    newSlot.position = newPosition;

                    currentItemsInScene.Add(newSlot);
                }
            }
        }


    }


}

