using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;
using RPG.Control;

namespace RPG.UI
{
    public class PlayerNameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameText = null;

        GameObject playerCharacterGameObject = null;

        private void OnEnable()
        {
            playerCharacterGameObject = PlayerController.GetFirstSelectedPlayer();
            RedrawUI();
        }

        public void RedrawUI()
        {
            Debug.Log("Player name ui redraw");
            CharacterSheet characterSheet = playerCharacterGameObject.GetComponent<CharacterSheet>();
            if (characterSheet != null)
            {
                nameText.text = characterSheet.CharacterName; 
            }

        }


    }

}



