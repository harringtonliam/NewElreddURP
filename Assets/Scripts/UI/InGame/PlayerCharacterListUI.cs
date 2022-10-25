using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.UI.InGame
{
    public class PlayerCharacterListUI : MonoBehaviour
    {
        [SerializeField]  PlayerCharacterUI playerCharacterUIPrefab = null;

        // Start is called before the first frame update
        void Start()
        {
            Redraw();
        }



        private void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            PlayerCharacter playerCharacter = FindObjectOfType<PlayerCharacter>();
            if (playerCharacter == null) return;
            foreach (var character in playerCharacter.PlayerCharacterGameObjects)
            {
                if (playerCharacterUIPrefab != null)
                {
                    var playerCharaceterUi = Instantiate(playerCharacterUIPrefab, transform);
                    playerCharaceterUi.SetUp(character);
                }
            }
        }
    }
}


