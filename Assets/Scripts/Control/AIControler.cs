using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using RPG.Attributes;
using RPG.SceneManagement;

namespace RPG.Control
{
    public class AIControler : MonoBehaviour
    {
        [SerializeField] AIRelationship aIRelationship = AIRelationship.Hostile;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] float aggrevationCoolDownTime = 2f;
        [SerializeField] bool usePatrolPath = true;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointPauseTime = 2f;
        [Range(0f, 1f)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float shoutDistance = 5f;
        [SerializeField] GameObject combatTargetGameObject;
        [SerializeField] InScenePortal targetPortal;

        GameObject player;
        Mover mover;


        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;
        float timeAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        public AIRelationship AIRelationship
        {
            get{ return aIRelationship;}
        }

        private void Awake()
        {
            player = PlayerSelector.GetFirstSelectedPlayer(); 
            mover = GetComponent<Mover>();
            if (aIRelationship == AIRelationship.Hostile)
            {
                combatTargetGameObject = player;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
              guardPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceAggrevated += Time.deltaTime;

            if (GetComponent<Health>().IsDead) return;

            if (InteractWithCombat()) return;
            if (InteractWithSuspicsion()) return;
            if (InteractWithInScenePortal()) return;
            if (InteractWithPatrolPath()) return;
            if (InteractWithGuardPosition()) return;
        }



        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }

        public void SetChaseDistance(float newChaseDistance)
        {
            chaseDistance = newChaseDistance;
        }

        public void SetPatrolPath(PatrolPath newPatrolPath, bool useNewPatrolPath)
        {
            usePatrolPath = useNewPatrolPath;
            if (patrolPath != newPatrolPath)
            {
                patrolPath = newPatrolPath;
                currentWaypointIndex = 0;
            }
        }

        public void SetWayPointPauseTime(float pauseTime)
        {
            waypointPauseTime = pauseTime;
        }

        public void SetPatrolSpeedFraction(float speedFraction)
        {
            patrolSpeedFraction = speedFraction;
        }

        public void SetCombatTarget(GameObject target)
        {
            combatTargetGameObject = target;
        }

        public void SetTargetInScenePortal(InScenePortal inScenePortal)
        {
            targetPortal = inScenePortal;
        }

        private bool InteractWithPatrolPath()
        {
            if (patrolPath == null) return false;
            if (!usePatrolPath) return false;

            timeAtWaypoint += Time.deltaTime;

            if (AtWaypoint())
            {
                timeAtWaypoint = 0;
                CycleWaypoint();
            }

            if (timeAtWaypoint > waypointPauseTime)
            {
                mover.StartMovementAction(GetCurrentWaypoint(), patrolSpeedFraction);
            }

            return true;
        }

        private bool AtWaypoint()
        {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            if (distanceToWayPoint <= waypointTolerance)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        
        private bool InteractWithSuspicsion()
        {
            if (timeSinceLastSawPlayer < suspicionTime )
            {
                ActionScheduler actionSchduler = GetComponent<ActionScheduler>();
                actionSchduler.CancelCurrentAction();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool InteractWithGuardPosition()
        {
            mover.StartMovementAction(guardPosition, patrolSpeedFraction);
            return true;
        }

        private bool InteractWithCombat()
        {

            Fighting fighter = GetComponent<Fighting>();
            if (combatTargetGameObject == null)
            {
                fighter.Cancel();
                return false;
            }
            if (IsAggrevated() && fighter.CanAttack(combatTargetGameObject))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(combatTargetGameObject);
                AggrevateNearbyEnemies();
                return true;
            }
            else
            {
                fighter.Cancel();
                return false;
            }
        }

        private bool InteractWithInScenePortal()
        {
            if (targetPortal == null) return false;
            if (AtPortal())
            {
                targetPortal.ActivatePortal(gameObject);
                targetPortal = null;
                return false;
            }
            mover.StartMovementAction(targetPortal.transform.position, patrolSpeedFraction);
            return true;
        }

        private bool AtPortal()
        {
            float distanceToPortal = Vector3.Distance(transform.position, targetPortal.transform.position);
            if (distanceToPortal <= waypointTolerance)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void AggrevateNearbyEnemies()
        {
            if (aIRelationship != AIRelationship.Hostile) return;

            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0f);
            foreach (var hit in hits)
            {

                AIControler ai = hit.transform.GetComponent<AIControler>();
                if (ai != null && ai != this)
                {
                    ai.Aggrevate();
                }
            }

        }

        private bool IsAggrevated()
        {
            if (timeSinceAggrevated < aggrevationCoolDownTime)
            {
                //aggrevated
                return true;
            }
            return DistanceToCombatTarget() <= chaseDistance;
        }

        private float DistanceToCombatTarget()
        {
            if (combatTargetGameObject == null) return Mathf.Infinity;
            float distance = Vector3.Distance(combatTargetGameObject.transform.position, transform.position);
            return distance;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
