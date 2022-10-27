using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxPathLength = 40f;
        [SerializeField] int maxCombatMovmentSquares = 6;
        [SerializeField] AudioSource footStepSound;

        NavMeshAgent navMeshAgent;
        Health health;
        private GridPosition gridPosition;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(LevelGrid.Instance.GetGridPosition(transform.position), this);
        }

        // Update is called once per frame
        void Update()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPoistion(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }

            navMeshAgent.enabled = !health.IsDead;
            UpdateAnimator();

        }

        private void UpdateAnimator()
        {
            //Global Velocity
            Vector3 velocity = navMeshAgent.velocity;
            //local character velocity 
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //forward speed
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, navMeshPath);
            if (!hasPath) return false;
            if (navMeshPath.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(navMeshPath) > maxPathLength) return false;
            if (!IsValidActionGridPosition(destination)) return false;
            return true;
        }



        public void StartMovementAction(Vector3 destination, float speedFraction)
        {

            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        //AnimationEvents
        public void FootR()
        {
            PlayFootStepSound();
        }
        public void FootL()
        {
            PlayFootStepSound();
        }

        private void PlayFootStepSound()
        {
            if (footStepSound != null)
            {
                footStepSound.Play();
            }
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }


        public object CaptureState()
        {
            //can also do this usinga struct
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }
        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;

        }

        private float GetPathLength(NavMeshPath navMeshPath)
        {
            float totalPathLength = 0f;

            if (navMeshPath.corners.Length < 2)
            {
                return totalPathLength;
            }

            for (int i = 0; i < navMeshPath.corners.Length - 1; i++)
            {
                float distance = Vector3.Distance(navMeshPath.corners[i], navMeshPath.corners[i + 1]);
                totalPathLength += distance;
            }

            return totalPathLength;
        }

        public bool IsValidActionGridPosition(GridPosition checkGridPosition)
        {
            List<GridPosition> validGridPositions = GetValidActionGridPostionList();
            return validGridPositions.Contains(checkGridPosition);
        }

        public bool IsValidActionGridPosition(Vector3 checkDestination)
        {
            GridPosition checkGridPosition = LevelGrid.Instance.GetGridPosition(checkDestination);
            List<GridPosition> validGridPositions = GetValidActionGridPostionList();
            return validGridPositions.Contains(checkGridPosition);
        }

        public List<GridPosition> GetValidActionGridPostionList()
        {
            List <GridPosition> validGridPositionList = new List<GridPosition>();

            for (int x = -maxCombatMovmentSquares; x <= maxCombatMovmentSquares; x++)
            {
                for (int z = -maxCombatMovmentSquares; z <= maxCombatMovmentSquares; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = gridPosition + offsetGridPosition;
                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }
                    if (gridPosition == testGridPosition)
                    {
                        continue;
                    }
                    if (LevelGrid.Instance.HasAnyUnitOnThisGridPosition(testGridPosition))
                    {
                        continue;
                    }
                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }
    }
}
    
