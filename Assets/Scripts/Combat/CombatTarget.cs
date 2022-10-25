using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        [SerializeField] bool isActive = true;

        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public RaycastableReturnValue HandleRaycast(PlayerController playerController)
        {
            if (!IsHostile())
            {
                return RaycastableReturnValue.NoAction;
            }

            Fighting fighting = playerController.transform.GetComponent<Fighting>();
            if (fighting.CanAttack(gameObject)  && isActive)
            {
                //if (Input.GetMouseButtonDown(0))
                //{
                //    fighting.Attack(gameObject);
                //}
                return RaycastableReturnValue.AllPlayerCharacters;
            }
            return RaycastableReturnValue.NoAction;
        }

        public void HandleActivation(PlayerController playerController)
        {
            Fighting fighting = playerController.transform.GetComponent<Fighting>();
            if (fighting != null)
            {
                fighting.Attack(gameObject);
            }
        }

        private bool IsHostile()
        {
            AIControler aIControler = GetComponent<AIControler>();
            if (aIControler == null) return true;
            
            if (aIControler != null)
            {
                if (aIControler.AIRelationship == AIRelationship.Hostile)
                {
                    return true;
                }
            }
            

            return false;
        }
    }
}