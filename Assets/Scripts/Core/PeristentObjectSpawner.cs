using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {

        [SerializeField] GameObject peristentObjectPrefab;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned)
            {
                return;
            }

            SpawnPeristentObjects();

            hasSpawned = true;

        }

        private void SpawnPeristentObjects()
        {
            GameObject peristentObject =  Instantiate(peristentObjectPrefab, transform.position, Quaternion.identity);
            DontDestroyOnLoad(peristentObject);
        }
    }

}

