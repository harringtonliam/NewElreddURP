using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.InventoryControl
{
    [CreateAssetMenu(fileName = "HoldableItem", menuName = "Items/Holdable Item", order = 20)]
    public class HoldableItemConfig : EquipableItem
    {

        [SerializeField] AnimatorOverrideController itemOverrideController = null;
        [SerializeField] HoldableItem equipedPrefab = null;
        [SerializeField] bool isRightHanded = false;

        const string itemName = "HoldableItem";

        public HoldableItem Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldItem(rightHand, leftHand);

            HoldableItem holdableItem = null;

            if (equipedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                holdableItem = Instantiate(equipedPrefab, handTransform);
                holdableItem.gameObject.name = itemName;
            }
            if (itemOverrideController != null)
            {
                animator.runtimeAnimatorController = itemOverrideController;
            }

            return holdableItem;

        }

        private void DestroyOldItem(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(itemName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(itemName);
            }
            if (oldWeapon == null)
            {
                return;
            }
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }

            return handTransform;
        }

    }
}


