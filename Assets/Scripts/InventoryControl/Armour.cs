using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;


namespace RPG.InventoryControl
{ 
    [CreateAssetMenu(fileName = "Armour", menuName = "Items/Make New Armour", order = 3)]
    public class Armour : EquipableItem
    {
        [SerializeField] int armourClassBonus = 0;
        [SerializeField] int maxDexBonus = 0;

        public int ArmourClassBonus
        {
            get { return armourClassBonus; }
        }

        public int MaxDexBonus
        {
            get { return maxDexBonus; }
        }
    }
}
