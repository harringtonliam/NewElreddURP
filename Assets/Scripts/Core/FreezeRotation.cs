using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FreezeRotation : MonoBehaviour
    {
        Rigidbody m_Rigidbody;

        private void Start()
        {
            //Fetch the Rigidbody from the GameObject with this script attached
            m_Rigidbody = GetComponent<Rigidbody>();
            //Stop the Rigidbody from rotating
            m_Rigidbody.freezeRotation = true;
        }


    }
}


