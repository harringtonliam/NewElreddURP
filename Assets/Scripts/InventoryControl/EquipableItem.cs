using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.InventoryControl
{
    [CreateAssetMenu(menuName = ("Items/Equipable Item"))]
    public class EquipableItem : InventoryItem
    {
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.Weapon;

        public EquipLocation AllowedEquiplocation
        {
            get { return allowedEquipLocation;  }
        }

    }
}

