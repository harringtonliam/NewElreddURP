using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Ammunition", menuName = "Weapons/Make New Ammunition", order = 1)]
    public class Ammunition : EquipableItem
    {
        //config
        [SerializeField] AmmunitionType ammunitionType = AmmunitionType.None;

        //Properties
        public AmmunitionType AmmunitionType {  get { return ammunitionType;  } }


    }


}


