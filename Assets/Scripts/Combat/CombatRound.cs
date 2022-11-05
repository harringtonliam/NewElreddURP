using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class CombatRound : MonoBehaviour
    {
        [SerializeField] int initiativeDiceRollSize = 20;


        private bool isCombatStarted = false;

        public bool IsCombatStarted {  get { return isCombatStarted; } set { isCombatStarted = value; } }

        private List<CombatParticipant> combatParticipants;
        private List<CombatParticipant> combatParticipantsInitiatveOrder;

        struct CombatParticipant
        {
            public Fighting fighting;
            public int InitiatveRoll;
        }


        private Dice dice;

        private int combatRoundNumber = 1;
        private int whosTurnIsItIndex = 0;
        

        

        // Start is called before the first frame update
        void Start()
        {
            dice = FindObjectOfType<Dice>();
            StartCombat();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartCombat()
        {
            isCombatStarted = true;
            combatRoundNumber = 1;

            RollInitiativeForEveryone();
            DebugInitiativeScores();
        }

        public Fighting GetItemsWhosTurnItIs()
        {
            return combatParticipantsInitiatveOrder[whosTurnIsItIndex].fighting;
        }

        private void DebugInitiativeScores()
        {
            foreach (var item in combatParticipantsInitiatveOrder)
            {
                Debug.Log("INitiavte: " + item.fighting.name + " : " + item.InitiatveRoll);
            }
        }

        private void RollInitiativeForEveryone()
        {
            Fighting[] allFightings = FindObjectsOfType<Fighting>();

            combatParticipants = new List<CombatParticipant>();
            foreach (var fighting in allFightings)
            {
                combatParticipants.Add(new CombatParticipant { fighting = fighting, InitiatveRoll = RollInitiative() });
            }

            combatParticipantsInitiatveOrder = combatParticipants.OrderByDescending(i => i.InitiatveRoll).ToList();
        }

        private void StartCombatRound()
        {
            whosTurnIsItIndex = 0;

        }

        private void StartNextTurn()
        {
            whosTurnIsItIndex++;
            if (whosTurnIsItIndex > combatParticipantsInitiatveOrder.Count)
            {
                StartCombat();
            }
        }

        public void EndCombat()
        {
            isCombatStarted = false;
        }

        private int RollInitiative()
        {
            return dice.RollDice(initiativeDiceRollSize, 1);
        }
    }

}


