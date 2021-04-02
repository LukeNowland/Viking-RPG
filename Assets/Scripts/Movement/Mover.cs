using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float maxSpeed = 5.66f;

        NavMeshAgent agent;
        Animator animator;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            agent.enabled = !GetComponent<Health>().IsDead();

            UpdateAnimation();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        private void UpdateAnimation()
        {
            Vector3 velocity = agent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            animator.SetFloat("forwardSpeed", localVelocity.z);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            agent.destination = destination;  
            agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);  
            agent.isStopped = false;
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }

        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            var data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
