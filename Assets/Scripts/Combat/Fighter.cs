using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 2f;

        Transform target = null;
        Mover mover = null;
        Animator animator = null;
        float timeSinceLastAttack = 0f;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        //Animation Event
        void Hit()
        {

        }
    }
}
