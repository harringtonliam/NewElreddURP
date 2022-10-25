using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Attributes;

namespace RPG.InventoryControl
{
    [RequireComponent(typeof(Inventory))]
    public class Container : MonoBehaviour,  IRaycastable
    {
        [SerializeField] bool alwaysAvailableToRaycast = false;

        bool isOpen = false;
        ContainerLink containerLink = null;
        

        private void Start()
        {
            containerLink = FindObjectOfType<ContainerLink>();
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }

        public RaycastableReturnValue HandleRaycast(PlayerController playerController)
        {
            if (!IsDead()  && !alwaysAvailableToRaycast)
            {
                return RaycastableReturnValue.NoAction;
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    ContainerOpener containerOpener = playerController.transform.GetComponent<ContainerOpener>();
            //    if (containerOpener != null)
            //    {
            //        containerOpener.StartOpenContainer(gameObject);
            //    }
            //}
            return RaycastableReturnValue.FirstPlayerCharacter;
        }

        public void HandleActivation(PlayerController playerController)
        {
            ContainerOpener containerOpener = playerController.transform.GetComponent<ContainerOpener>();
            if (containerOpener != null)
            {
                containerOpener.StartOpenContainer(gameObject);
            }
        }

        public void OpenContainer()
        {
            if (isOpen) return;

            isOpen = true;
            if (containerLink != null)
            {
                containerLink.OpenContainer(this);
            }
            
        }

        public void CloseContainer()
        {
            if (!isOpen) return;

            isOpen = false;
            if (containerLink != null)
            {
                containerLink.CloseContainer();
            }
        }

        private bool IsDead()
        {
            Health aiHealth = GetComponent<Health>();
            if (aiHealth == null) return true;
            if (aiHealth.IsDead)
            {
                    return true;
            }
            return false;
        }



    }
}

