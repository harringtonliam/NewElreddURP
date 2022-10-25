using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.InventoryControl;
using RPG.Combat;

namespace RPG.UI.InventoryControl
{
    public class InventoryItemDetailsUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI displayNameText = null;
        [SerializeField] TextMeshProUGUI descriptionText = null;
        [SerializeField] TextMeshProUGUI weaponDamageText = null;
        [SerializeField] TextMeshProUGUI weaponToHitBonusText = null;
        [SerializeField] TextMeshProUGUI weaponDamageBonusText = null;
        [SerializeField] TextMeshProUGUI weaponRangeText = null;
        [SerializeField] TextMeshProUGUI modifierAbilityText = null;
        [SerializeField] TextMeshProUGUI ammoTypeText = null;
        [SerializeField] TextMeshProUGUI armourClassBonus = null;
        [SerializeField] TextMeshProUGUI maxDexBonus = null;
        [SerializeField] TextMeshProUGUI healingAmountText = null;
        [SerializeField] TextMeshProUGUI healingBonusText = null;
        [SerializeField] GameObject displayArea;
        [SerializeField] GameObject weaponDisplayArea;
        [SerializeField] GameObject armourDisplayArea;
        [SerializeField] GameObject medicalPackDisplayArea;


        public void Setup(InventoryItem inventoryItem)
        {
            displayNameText.text = inventoryItem.DisplayName;
            descriptionText.text = inventoryItem.Description;

            DisplayWeaponConfig(inventoryItem);

            DisplayArmour(inventoryItem);

            DisplayMedicalPack(inventoryItem);

            displayArea.SetActive(true);
        }

        private void DisplayMedicalPack(InventoryItem inventoryItem)
        {
            MedicalPack medicalPack = inventoryItem as MedicalPack;
            if (medicalPack != null)
            {
                string healingAmountString;
                if (medicalPack.MedicalPackHealingDiceNumber > 1)
                {
                    healingAmountString = "(1-" + medicalPack.MedicalPackHealingDice.ToString() + ") x " + medicalPack.MedicalPackHealingDiceNumber.ToString();
                }
                else
                {
                    healingAmountString = "1-" + medicalPack.MedicalPackHealingDice.ToString();
                }

                healingAmountText.text = healingAmountString;
                if (medicalPack.MedicalPackHealingAdditiveBonus >= 0)
                {
                    healingBonusText.text = "+" + medicalPack.MedicalPackHealingAdditiveBonus.ToString();
                }
                else
                {
                    healingBonusText.text = "-" + medicalPack.MedicalPackHealingAdditiveBonus.ToString();
                }
                medicalPackDisplayArea.SetActive(true);
            }
            else
            {
                medicalPackDisplayArea.SetActive(false);
            }
        }

        private void DisplayArmour(InventoryItem inventoryItem)
        {
            Armour armour = inventoryItem as Armour;
            if (armour != null)
            {
                if (armour.ArmourClassBonus > 0)
                {
                    armourClassBonus.text = "+" + armour.ArmourClassBonus.ToString();
                }
                else
                {
                    armourClassBonus.text = "-" + armour.ArmourClassBonus.ToString();
                }
                if (armour.MaxDexBonus > 0)
                {
                    maxDexBonus.text = "+" + armour.MaxDexBonus.ToString();
                }
                else
                {
                    maxDexBonus.text = armour.MaxDexBonus.ToString();
                }
                armourDisplayArea.SetActive(true);
            }
            else
            {
                armourDisplayArea.SetActive(false);
            }
        }

        private void DisplayWeaponConfig(InventoryItem inventoryItem)
        {
            WeaponConfig weaponConfig = inventoryItem as WeaponConfig;
            if (weaponConfig != null)
            {

                string weaponDamageString;
                if (weaponConfig.WeaponDamageDiceNumber > 1)
                {
                    weaponDamageString = "(1-" + weaponConfig.WeaponDamage.ToString() + ") x " + weaponConfig.WeaponDamageDiceNumber.ToString();
                }
                else
                {
                    weaponDamageString = "1-" + weaponConfig.WeaponDamage.ToString();
                }

                weaponDamageText.text = weaponDamageString;
                if (weaponConfig.WeaponToHitBonus >= 0)
                {
                    weaponToHitBonusText.text = "+" + weaponConfig.WeaponToHitBonus.ToString();
                }
                else
                {
                    weaponToHitBonusText.text = "-" + weaponConfig.WeaponToHitBonus.ToString();
                }
                if (weaponConfig.WeaponDamageAdditiveBonus >= 0)
                {
                    weaponDamageBonusText.text = "+" + weaponConfig.WeaponDamageAdditiveBonus.ToString();
                }
                else
                {
                    weaponDamageBonusText.text = "-" + weaponConfig.WeaponDamageAdditiveBonus.ToString();
                }
                weaponRangeText.text = weaponConfig.WeaponRange.ToString();
                modifierAbilityText.text = weaponConfig.ModifierAbility.ToString();
                ammoTypeText.text = weaponConfig.AmmunitionType.ToString();

                weaponDisplayArea.SetActive(true);
            }
            else
            {
                weaponDisplayArea.SetActive(false);
            }
        }
    }

}


