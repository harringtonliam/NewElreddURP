using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;


namespace RPG.UseablePropControl
{
    public class UseProp : MonoBehaviour, IAction
    {
        [SerializeField] float useRange = 1f;


        UseableProp target;

        public event Action onUsePropCancel; 

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

                    UseAblePropBehaviour();
                }
            }
        }

        private void UseAblePropBehaviour()
        {
            transform.LookAt(target.transform);
            target.UseProp();
            //Liam 13/03/2022 don't think this is needed.  Removed to allow fr UI to hide when player moves away from prop
            //GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void StartUseProp(GameObject useAbleProp)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = useAbleProp.GetComponent<UseableProp>(); ;
        }


        public void Cancel()
        {
            target = null;
            GetComponent<Mover>().Cancel();
            if (onUsePropCancel != null)
            {
                onUsePropCancel();
            }
        }



        private bool GetIsInRange()
        {

            bool isInRange = useRange >= Vector3.Distance(target.transform.position, transform.position);
            
            return isInRange;
        }
    }

}


