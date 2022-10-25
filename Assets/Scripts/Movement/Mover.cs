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
        [SerializeField] AudioSource footStepSound;

        NavMeshAgent navMeshAgent;
        Health health;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {

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
    }
}
    
