using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventoryControl;
using RPG.Combat;
using TMPro;

namespace RPG.UI.InventoryControl
{
    public class ArmourClassUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI armourClassText;


        ArmourClass playerAmourClass;
        Equipment playerEquipment;

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            playerAmourClass = player.GetComponent<ArmourClass>();
            playerEquipment = player.GetComponent<Equipment>();
            playerEquipment.equipmentUpdated += RedrawUI;
        }

        // Start is called before the first frame update
        void Start()
        {
            RedrawUI();
        }



        private void RedrawUI()
        {
            if (armourClassText == null) return;

            armourClassText.text = playerAmourClass.CalculateArmourClass().ToString(); ;

        }
    }
}


