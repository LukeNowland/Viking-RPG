using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Transform target = null;
        Mover mover = null;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (target != null)
            {
                mover.MoveTo(target.position);
                FindStoppingDistance();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        void FindStoppingDistance()
        {
            var targetDistance = Vector3.Distance(transform.position, target.position);
            if (targetDistance <= weaponRange)
            {
                mover.StopMoving();
            }
        }
    }
}
