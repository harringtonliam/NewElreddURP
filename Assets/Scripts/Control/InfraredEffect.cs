using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using System;
using RPG.Core;

namespace RPG.Control
{
    public class InfraredEffect : MonoBehaviour
    {
        [SerializeField] GameObject infraRedEffect;
        [SerializeField] string infraReditemId = "a7e50ca9-d716-497b-9513-b70df3821bc9";

        Equipment playerEquipment;
        InGameSettings inGameSettings;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerEquipment = player.GetComponent<Equipment>();
            playerEquipment.equipmentUpdated += CheckForInfrared;
        }

        private void Start()
        {
            inGameSettings = FindObjectOfType<InGameSettings>();
            inGameSettings.settingsUpdated += CheckForInfrared;
            CheckForInfrared();
        }

        private void CheckForInfrared()
        {
            InventoryItem headItem = playerEquipment.GetItemInSlot(EquipLocation.Helmet);
            string foundId = string.Empty;
            if (headItem != null)
            {
                foundId = headItem.ItemID;
            }

            if (foundId == infraReditemId  && inGameSettings.IsNightVisionOn)
            {
                SwitchEffect(true);
            }
            else
            {
                SwitchEffect(false);
            }
        }

        private  void SwitchEffect(bool isEffectOn)
        {
            if (infraRedEffect == null) return;

            infraRedEffect.SetActive(isEffectOn);
        }
    }
}


