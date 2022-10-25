using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class SweepoController : MonoBehaviour
    {
        [SerializeField] WeaponConfig weaponToLookFor;

        Inventory inventory;
        WeaponStore weaponStore;

        // Start is called before the first frame update
        void Awake()
        {
            inventory = GetComponent<Inventory>();
            weaponStore = GetComponent<WeaponStore>();
            inventory.inventoryUpdated += CheckForWeapon;
        }

        private void CheckForWeapon()
        {
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                if (inventory.GetItemInSlot(i) != null && inventory.GetItemInSlot(i).ItemID == weaponToLookFor.ItemID)
                {
                    weaponStore.AddAction(weaponToLookFor, 0, 1, true);
                }
            }
        }
    }

}


