using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.InventoryControl
{
    [CreateAssetMenu(menuName = ("Items/Inventory Item"))]
    public class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {

        // Configuration
        [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
        [SerializeField] string itemID = null;
        [Tooltip("Item name to be displayed in UI.")]
        [SerializeField] string displayName = null;
        [Tooltip("Item description to be displayed in UI.")]
        [SerializeField] [TextArea] string description = null;
        [Tooltip("The UI icon to represent this item in the inventory.")]
        [SerializeField] Sprite icon = null;
        [Tooltip("The pickup prefab that should be spawned when this item is dropped.")]
        [SerializeField] Pickup pickup = null;
        [Tooltip("If true, multiple items of this type can be stacked in the same inventory slot.")]
        [SerializeField] bool isStackable = false;
        [Tooltip("For stackable items this is the max number of items of this type can be stacked in the same inventory slot.")]
        [SerializeField] int maxNumberInStack = 20;

        public string  ItemID { get { return itemID; } }
        public string DisplayName {  get { return displayName; } }
        public string Description { get { return description; } }
        public Sprite Icon { get { return icon; } }
        public bool IsStackable {get {return isStackable;} }
        public int MaxNumberInStack {  get { return maxNumberInStack; } }

        static Dictionary<string, InventoryItem> itemLookupCache;

        public static InventoryItem GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache.Add(item.itemID, item);
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID))
            {
                return null;
            }

            return itemLookupCache[itemID];
        }


        public Pickup SpawnPickup(Vector3 position, int number)
        {
            var pickup = Instantiate(this.pickup);
            pickup.transform.position = position;
            pickup.Setup(this, number);
            return pickup;
        }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        public void OnBeforeSerialize()
        {

        }
    }
}


