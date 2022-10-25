using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class Door : MonoBehaviour, IDoor
    {
        [SerializeField] Transform door = null;
        [SerializeField] Transform openPosition = null;
        [SerializeField] Transform closedPosition = null;
        [SerializeField] bool isLocked = false;
        [SerializeField] float doorSpeed = 0.5f;

        bool opening = false;
        bool closing = false;


        private void Update()
        {
            if (opening)
            {
                OpenDoor();
            }
            else if (closing)
            {
                CloseDoor();
            }
            
        }



        private void OnTriggerEnter(Collider other)
        {
            if (isLocked) return;

            if (door == null  || openPosition == null) return;

            closing = false;
            opening = true;

        }


        private void OnTriggerExit(Collider other)
        {
            if (isLocked) return;

            if (door == null || closedPosition == null) return;

            opening = false;
            closing = true;

        }


        private void CloseDoor()
        {
            door.position = Vector3.MoveTowards(door.position, closedPosition.position, doorSpeed * Time.deltaTime);
            float distanceToClosed = Vector3.Distance(door.position, closedPosition.position);
            if (Mathf.Approximately(distanceToClosed, 0f))
            {
                closing = false;
            }
        }

        private void OpenDoor()
        {
            door.position = Vector3.MoveTowards(door.position, openPosition.position, doorSpeed * Time.deltaTime);
            float distanceToClosed = Vector3.Distance(door.position, openPosition.position);
            if (Mathf.Approximately(distanceToClosed, 0f))
            {
                opening = false;
            }
        }

        public void Lock()
        {
            isLocked = true;
        }

        public void UnLock()
        {
            isLocked = false;
        }
    }

}


