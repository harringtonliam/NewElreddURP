using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace RPG.Attributes
{
    public class PlayerCharacter : MonoBehaviour
    {
        List<GameObject> playerCharacterGameObjects;


        public List<GameObject> PlayerCharacterGameObjects {  get { return playerCharacterGameObjects; } }

 
        void Awake()
        {
            playerCharacterGameObjects = GetPlayerCharacterGameObjects();
        }


        public void AddPlayerCharacter(GameObject newPlayer)
        {
            playerCharacterGameObjects.Add(newPlayer);
        }

        public void RemovePlayerCharacter(GameObject exPlayer)
        {
            //TODO

        }
        

        private List<GameObject> GetPlayerCharacterGameObjects()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            return players.ToList<GameObject>();
        }
    }

}


