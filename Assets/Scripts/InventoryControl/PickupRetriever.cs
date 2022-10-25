using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.InventoryControl
{
    public class PickupRetriever : MonoBehaviour, IAction
    {
        [SerializeField] float pickUpRange = 1f;

        Pickup target;



        // Update is called once per frame
        void Update()
        {
            Mover mover = GetComponent<Mover>();

            if (target != null)
            {
                mover.MoveTo(target.transform.position, 1f); ;
                if (GetIsInRange())
                {
                    mover.Cancel();

                    PickupBehaviour();
                }
            }
        }

        private void PickupBehaviour()
        {
            transform.LookAt(target.transform);
            target.PickupItem(GetComponent<Inventory>());

        }

        public void StartPickupRetrieval(GameObject pickup)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = pickup.GetComponent<Pickup>(); 
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private bool GetIsInRange()
        {
            return pickUpRange >= Vector3.Distance(target.transform.position, transform.position);
        }
    }
}