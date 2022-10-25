using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{



    public class ConstantRotation : MonoBehaviour
    {
        [SerializeField] float yRotationDegrees = 10f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, yRotationDegrees * Time.deltaTime, 0);
        }
    }
}


