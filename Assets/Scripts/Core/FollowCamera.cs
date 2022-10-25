using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{


    public class FollowCamera : MonoBehaviour
    {

        [SerializeField] Transform target;
        // Start is called before the first frame update
        void Start()
        {

        }

        // LateUpdate is called after update, so camera only moves after player
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
