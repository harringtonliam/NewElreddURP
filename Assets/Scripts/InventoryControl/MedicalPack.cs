using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Core;

namespace RPG.InventoryControl
{
    [CreateAssetMenu(menuName = ("Items/MedicalPack"))]
    public class MedicalPack : ActionItem
    {
        [SerializeField] int medicalPackHealingDice = 8;
        [SerializeField] int medicalPackHealingDiceNumber = 1;
        [SerializeField] int medicalPackHealingAdditiveBonus = 0;
        [SerializeField] GameObject useFX = null;


        public int MedicalPackHealingDice
        {
            get { return medicalPackHealingDice; }
        }

        public int MedicalPackHealingDiceNumber
        {
            get { return medicalPackHealingDiceNumber; }
        }

        public int MedicalPackHealingAdditiveBonus
        {
            get { return medicalPackHealingAdditiveBonus; }
        }

        public override void Use(GameObject user)
        {
            Health health = user.GetComponent<Health>();
            if (health == null) return;

            ActionScheduler actionScheduler = user.GetComponent<ActionScheduler>();
            if (actionScheduler != null)
            {
                actionScheduler.CancelCurrentAction();
            }

            Dice dice = FindObjectOfType<Dice>();

            int healingAmount = dice.RollDice(medicalPackHealingDice, medicalPackHealingDiceNumber) + medicalPackHealingAdditiveBonus;

            health.Heal(healingAmount);
            PlayFx();
            WriteToConsole("Medical Pack Used: healed " + healingAmount.ToString() + " hit points.");

        }

        private void WriteToConsole(string textToWrite)
        {
            GameConsole gameConsole = FindObjectOfType<GameConsole>();
            if (gameConsole == null) return;

            gameConsole.AddNewLine(textToWrite);

        }

        private void PlayFx()
        {
            if (useFX == null) return;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject fx = GameObject.Instantiate(useFX, player.transform);
            Destroy(fx, 1f);

        }

    }
}


