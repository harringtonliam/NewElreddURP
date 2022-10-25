using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.InventoryControl
{
    public class ContainerOpener : MonoBehaviour, IAction
    {
        [SerializeField] float openRange = 1f;

        Container target;

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
                    OpenBehaviour();
                }
            }
        }

        private void OpenBehaviour()
        {
            transform.LookAt(target.transform);
            target.OpenContainer();
        }

        public void StartOpenContainer(GameObject container)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = container.GetComponent<Container>(); ;
        }


        public void Cancel()
        {
            target.CloseContainer();
            target = null;
            GetComponent<Mover>().Cancel();
        }



        private bool GetIsInRange()
        {
            return openRange >= Vector3.Distance(target.transform.position, transform.position);
        }
    }


}

