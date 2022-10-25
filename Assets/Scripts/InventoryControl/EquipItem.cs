using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Combat;

namespace RPG.InventoryControl
{



    public class EquipItem : MonoBehaviour, ISaveable
    {
        [SerializeField] HoldableItemConfig defaultHoldableItem;

        private HoldableItemConfig currentHoldableItemConfig;
        HoldableItem currentHoldableItem;
        Equipment equipment;
        Transform rightHandTransform;
        Transform leftHandTransform;


        private void Awake()
        {
            equipment = GetComponent<Equipment>();

            if (equipment)
            {
                equipment.equipmentUpdated += UpdateEquipment;
            }
        }

        void Start()
        {
            GetHandTransforms();
            if (currentHoldableItemConfig == null)
            {
                currentHoldableItem = EquipHoldableItem(defaultHoldableItem);
            }
        }

        private void GetHandTransforms()
        {
            Fighting fighting = GetComponent<Fighting>();
            rightHandTransform = fighting.RightHAdnTransform;
            leftHandTransform = fighting.LeftHandTransform;
        }

        private void UpdateEquipment()
        {
            var holdAbleItemConfig = (HoldableItemConfig)equipment.GetItemInSlot(EquipLocation.Shield);
            if (holdAbleItemConfig != null)
                EquipHoldableItem(holdAbleItemConfig);
        }

        public HoldableItem EquipHoldableItem(HoldableItemConfig holdableItemConfig)
        {
            currentHoldableItemConfig = holdableItemConfig;
            Animator animator = GetComponent<Animator>();
            currentHoldableItem = currentHoldableItemConfig.Spawn(rightHandTransform, leftHandTransform, animator);
            return currentHoldableItem;
        }

        public object CaptureState()
        {
            if (currentHoldableItemConfig != null)
            {
                return currentHoldableItemConfig.name;
            }
            else
            {
                return null;
            }
        }

        public void RestoreState(object state)
        {
            if (state != null)
            {
                string weaponName = (string)state;
                HoldableItemConfig holdableItemConfig = Resources.Load<HoldableItemConfig>(weaponName);
                EquipHoldableItem(holdableItemConfig);
            }
        }
    }

}

