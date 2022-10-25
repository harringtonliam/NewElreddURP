using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using UnityEngine.UI;
using RPG.UI.InventoryControl;

namespace RPG.UI.InGame
{
    public class QuickWeaponSlotUI : MonoBehaviour
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;
        [SerializeField] GameObject activeIndicator;


        //cache
        GameObject player = null;
        WeaponStore weaponStore = null;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            weaponStore = player.GetComponent<WeaponStore>();
            weaponStore.storeUpdated += UpdateIcon;
        }

        public WeaponConfig GetItem()
        {
            return weaponStore.GetAction(index);
        }

        public int GetNumber()
        {
            return weaponStore.GetNumber(index);
        }


        public void EquipWeapon()
        {
            weaponStore.SetActiveWeapon(index);
        }

        private void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
            if (weaponStore.GetActiveWeaponIndex() == index)
            {
                activeIndicator.SetActive(true);
            }
            else
            {
                activeIndicator.SetActive(false);
            }
        }


    }

}


