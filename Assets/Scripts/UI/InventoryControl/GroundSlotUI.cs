using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.UI.Dragging;


namespace RPG.UI.InventoryControl
{


    public class GroundSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        ScenePickups scenePickups;
        GameObject player;


        void Start()
        {
            scenePickups = FindObjectOfType<ScenePickups>();
            player = GameObject.FindWithTag("Player");
        }


        public void AddItems(InventoryItem item, int number)
        {
            if (scenePickups != null)
            {
                Vector3 dropPosition = player.transform.position + Vector3.forward;
                scenePickups.AddItem(item, number, dropPosition);
                Pickup newPickup = item.SpawnPickup(dropPosition, number);
                newPickup.transform.parent = scenePickups.transform;
            }
        }

        public InventoryItem GetItem()
        {
            return null;
        }

        public int GetNumber()
        {
            return 0;
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return int.MaxValue;
        }

        public void RemoveItems(int number)
        {
            
        }


    }
}

