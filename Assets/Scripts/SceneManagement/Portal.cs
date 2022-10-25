using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;
using RPG.Core;


namespace RPG.SceneManagement
{


    public class Portal : MonoBehaviour
    {
        [SerializeField] float fadeTime = 2f;

        enum DesintationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] string sceneToLoad = "Level2";
        [SerializeField] Transform spawnPoint;
        [SerializeField] DesintationIdentifier destinationIdentifier;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);


            SavingWrapper saveingWrapper = FindObjectOfType<SavingWrapper>();
            DisablePlayerControl();  //player on old scene
            saveingWrapper.AutoSave();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            DisablePlayerControl();  //player in new scene

            saveingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            saveingWrapper.AutoSave();

            yield return fader.FadeIn(fadeTime);

            EnablePlayerControl();
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
           foreach(Portal portal in   FindObjectsOfType<Portal>())
           {
                if (portal == this) continue;
                 if (portal.destinationIdentifier != destinationIdentifier) continue;

                return portal;
           }

            return null;
        }


        private void DisablePlayerControl()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnablePlayerControl()
        {
            try
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<PlayerController>().enabled = true;
            }
            catch(Exception ex)
            {
                Debug.Log("Portal EnablePlayerController "  + ex.Message);
            }
        }
    }

}
