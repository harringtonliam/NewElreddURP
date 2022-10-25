using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;


namespace RPG.SceneManagement
{

    public class PortalActivator : MonoBehaviour, IAction
    {
        [SerializeField] float activateRange = 1f;

        InScenePortal targetPortal;


        // Update is called once per frame
        void Update()
        {
            Mover mover = GetComponent<Mover>();

            if (targetPortal != null)
            {
                mover.MoveTo(targetPortal.transform.position, 1f); ;
                if (GetIsInRange())
                {
                    mover.Cancel();
                    PortalBehaviour();
                }
            }
        }

        public void SetTargetPortal(InScenePortal newPortal)
        {
            targetPortal = newPortal;
        }

        private void PortalBehaviour()
        {
            targetPortal.ActivatePortal(gameObject);
        }

        public void StartPortalActivation(GameObject portal)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetPortal = portal.GetComponent<InScenePortal>(); ;
        }

        public void Cancel()
        {
            targetPortal = null;
            GetComponent<Mover>().Cancel();
        }

        private bool GetIsInRange()
        {
            return activateRange >= Vector3.Distance(targetPortal.transform.position, transform.position);
        }
    }
}