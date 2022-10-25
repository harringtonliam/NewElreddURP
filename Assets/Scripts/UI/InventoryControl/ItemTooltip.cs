using UnityEngine;
using TMPro;
using RPG.InventoryControl;

namespace RPG.UI.InventoryControl
{
    /// <summary>
    /// Root of the tooltip prefab to expose properties to other classes.
    /// </summary>
    public class ItemTooltip : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] TextMeshProUGUI titleText = null;

        // PUBLIC

        public void Setup(InventoryItem item)
        {
            titleText.text = item.DisplayName;
        }
    }
}
