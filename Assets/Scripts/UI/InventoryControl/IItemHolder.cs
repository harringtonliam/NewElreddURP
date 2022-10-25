using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;

namespace RPG.UI.InventoryControl
{

    public interface IItemHolder 
    {
        InventoryItem GetItem();
    }
}


