using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.InventoryControl
{

    public class Pickup : MonoBehaviour, IRaycastable
    {
        //Config
        [SerializeField] InventoryItem inventoryItem = null;
        [SerializeField] int numberOfItems = 1;


        public InventoryItem InventoryItem { get { return inventoryItem; } }
        public int NumberOfItems { get { return numberOfItems; } }


        public void Setup(InventoryItem item, int number)
        {
            this.inventoryItem = item;
            if (!item.IsStackable)
            {
                number = 1;
            }

            this.numberOfItems = number;
        }

        public void PickupItem(Inventory inventory)
        {
            bool slotFoundOk = inventory.AddToFirstEmptySlot(inventoryItem, numberOfItems);
            if (slotFoundOk)
            {
                Destroy(gameObject);
                ScenePickups scenePickups = FindObjectOfType<ScenePickups>();
                if (scenePickups != null)
                {
                    scenePickups.RemoveItem(this.inventoryItem, this.numberOfItems, this.transform.position);
                }
            }
        }

        public bool CanBePickedUp(Inventory inventory)
        {
            return inventory.HasSpaceFor(inventoryItem);
        }

        public CursorType GetCursorType()
        {
                return CursorType.Pickup;

        }

        public RaycastableReturnValue HandleRaycast(PlayerSelector playerSelector)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    PickupRetriever pickupRetriever = playerController.transform.GetComponent<PickupRetriever>();
            //    if (pickupRetriever != null)
            //    {
            //        pickupRetriever.StartPickupRetrieval(gameObject);
            //    }
            //}
            return RaycastableReturnValue.FirstPlayerCharacter;
        }

        public void  HandleActivation(PlayerSelector playerSelector)
        {
            PickupRetriever pickupRetriever = playerSelector.transform.GetComponent<PickupRetriever>();
            if (pickupRetriever != null)
            {
                pickupRetriever.StartPickupRetrieval(gameObject);
            }
        }



    }

}


