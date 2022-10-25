using RPG.Attributes;
using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{

    public class TurrentGunController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float aggrevationCoolDownTime = 2f;

        float timeSinceAggrevated = Mathf.Infinity;

        GameObject player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }



        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Health>().IsDead) return;

            if (InteractWithCombat()) return;
        }


        private bool InteractWithCombat()
        {
            Fighting fighter = GetComponent<Fighting>();
            if (IsAggrevated() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
                return true;
            }
            else
            {
                fighter.Cancel();
                return false;
            }
        }

        private bool IsAggrevated()
        {
            if (timeSinceAggrevated < aggrevationCoolDownTime)
            {
                //aggrevated
                return true;
            }
            return DistanceToPlayer() <= chaseDistance;
        }

        private float DistanceToPlayer()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}


