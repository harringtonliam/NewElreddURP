using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.Control;
using RPG.Core;


namespace RPG.SceneManagement
{
    public class InScenePortal : MonoBehaviour, IRaycastable
    {
        [SerializeField] float fadeTime = 1f;

        [SerializeField] Transform spawnPoint;
        [SerializeField] InScenePortal destinationPortal;
        [SerializeField] bool playerUsablePortal = true;

        private void OnTriggerEnter(Collider other)
        {
           if (other.tag == "Player")
            {
                StartCoroutine(Transition(other.gameObject));
            }
           else
            {
                UpdatePortalActivator(other.gameObject);
            }
        }


        public void ActivatePortal(GameObject portalActivator)
        {
            if (!playerUsablePortal && portalActivator.GetComponent<PlayerController>() != null)
            {
                return;
            }

            if(portalActivator.GetComponent<PlayerController>() != null)
            {
                StartCoroutine(Transition(portalActivator));
            }
            else
            {
                UpdatePortalActivator(portalActivator);
            }
        }


        private IEnumerator Transition(GameObject portalActivator)
        {
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime);

            SavingWrapper saveingWrapper = FindObjectOfType<SavingWrapper>();
            DisablePlayerControl();
            UpdatePortalActivator(portalActivator);

            yield return fader.FadeIn(fadeTime);

            EnablePlayerControl();
        }

        private void UpdatePortalActivator(GameObject portalActivator)
        {

            portalActivator.GetComponent<NavMeshAgent>().Warp(destinationPortal.spawnPoint.position);
            portalActivator.transform.rotation = destinationPortal.spawnPoint.rotation;
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
            catch (Exception ex)
            {
                Debug.Log("InScenePortal EnablePlayerController " + ex.Message);
            }
        }

        public CursorType GetCursorType()
        {
                return CursorType.Pickup;
        }

        public RaycastableReturnValue HandleRaycast(PlayerController playerController)
        {
            if (!playerUsablePortal) return RaycastableReturnValue.NoAction;
            //if (Input.GetMouseButtonDown(0))
            //{
            //    PortalActivator portalActivator = playerController.transform.GetComponent<PortalActivator>();
            //    if (portalActivator != null)
            //    {
            //        portalActivator.StartPortalActivation(gameObject);
            //    }
            //}
            return RaycastableReturnValue.AllPlayerCharacters;
        }

        public void HandleActivation(PlayerController playerController)
        {
            PortalActivator portalActivator = playerController.transform.GetComponent<PortalActivator>();
            if (portalActivator != null)
            {
                portalActivator.StartPortalActivation(gameObject);
            }
        }

    }


}

