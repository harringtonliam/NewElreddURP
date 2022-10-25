using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.UI.InGame;

namespace RPG.UI.Information
{
    public class CharacterInfoUI : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlayerCharacter playerCharacter = FindObjectOfType<PlayerCharacter>();
            if(playerCharacter== null) return;
            GetComponent<PlayerCharacterUI>().SetUp(playerCharacter.PlayerCharacterGameObjects[0]);
        }
    }
}


