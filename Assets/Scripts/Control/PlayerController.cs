using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RPG.Movement;
using RPG.Core;
using System;
using RPG.Attributes;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {


        [System.Serializable]
        struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D texture2D;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float raycastRadius = 0.25f;

        Mover mover;
        PlayerSelector playerSelector;



        private void Awake()
        {
            mover = GetComponent<Mover>();
            playerSelector = GetComponent<PlayerSelector>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithUI()) return;

            if (!playerSelector.IsSelected) return;

            if (GetComponent<Health>().IsDead)
            {
                //SetCursorType(CursorType.None);
                return;
            }

            //if (InteractWithComponent()) return;
            //TODO  Because InteractWithComponent is happening in mouse controller InteractWithMovement is cancelling the previous action straight away
            //if (InteractWithMovement()) return;

            //SetCursorType(CursorType.None);
        }



       

        public void PlayerDead()
        {
            GameOver gameOver = FindObjectOfType<GameOver>();
            if (gameOver!= null)
            {
                gameOver.GameOverActions();
            }
        }

        //TODO Remove this.  onece MouseController is finished
        //private void SetCursorType(CursorType cursorType)
        //{
        //    CursorMapping cursorMapping = GetCursorMapping(cursorType);
        //    Cursor.SetCursor(cursorMapping.texture2D, cursorMapping.hotspot, CursorMode.Auto);
        //}

        private CursorMapping GetCursorMapping(CursorType cursorType)
        {
            foreach (var cursorMapping in cursorMappings)
            {
                if (cursorMapping.cursorType == cursorType)
                {
                    return cursorMapping;
                }
            }
            return new CursorMapping();
        }



        private bool InteractWithUI()
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            //{
            //    SetCursorType(CursorType.UI);
            //}

            return EventSystem.current.IsPointerOverGameObject();
        }


        //TODO: Renove this
        //private bool InteractWithComponent()
        //{

        //    RaycastHit[] hits = RaycastAllSorted();
        //    foreach (var hit in hits)
        //    {
        //        IRaycastable[] raycastables = hit.collider.gameObject.GetComponents<IRaycastable>();
        //        foreach (var raycastable in raycastables)
        //        {
        //            if (raycastable.HandleRaycast(this))
        //            {
        //                //SetCursorType(raycastable.GetCursorType());
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            //sort by distance
            float[] distances = new float[hits.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            //return sorted array
            return hits;
        }


        private bool InteractWithMovement()
        {

            Vector3 target;
            bool hashit = RaycastNavMesh(out target);
            if (hashit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false;
                if (Input.GetMouseButton(0))
                {
                    mover.StartMovementAction(target, 1f); ;
                }
                //SetCursorType(CursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            RaycastHit hit;
            target = new Vector3(0, 0, 0);
            bool hashit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hashit) return false;
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, 1f, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;
            target = navMeshHit.position;

            return true;
        }



        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }




    }
}
