using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{

    public class PatrolPath : MonoBehaviour
    {

        const float waypointSphereRadius = 0.5f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(GetWaypoint(i), waypointSphereRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextIndex(i)));
            }
        }

        public int GetNextIndex(int i)
        {
            int j = i + 1;
            if (j > transform.childCount -1)
            {
                j = 0;
            }

            return j;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}
