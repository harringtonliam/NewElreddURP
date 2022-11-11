using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using UnityEngine.UI;

namespace RPG.UI.Combat
{
    public class EndTurnButton : MonoBehaviour
    {

        private CombatRound combatRound;
        private Button button;

        // Start is called before the first frame update
        void Start()
        {
            combatRound = FindObjectOfType<CombatRound>();
            button = GetComponent<Button>();
            button.onClick.AddListener(EndTurn);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void EndTurn()
        {
            combatRound.EndTurn();
        }
    }

}

