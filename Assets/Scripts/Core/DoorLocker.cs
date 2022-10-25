using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DoorLocker : MonoBehaviour
    {
        [SerializeField] Door door = null;
        [SerializeField] bool lockDoor = true;


        private void OnTriggerEnter(Collider other)
        {
            if (door == null) return;
            if (other.gameObject.tag != "Player") return;

            LockOrUnlockDoor();

        }

        public void LockOrUnlockDoor()
        {
            if (lockDoor)
            {
                door.Lock();
            }
            else
            {
                door.UnLock();
            }
        }
    }

}


