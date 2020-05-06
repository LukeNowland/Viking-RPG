using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject player;
        Fighter fighter;
        Vector3 startPos;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            startPos = transform.position;
        }

        private void Update()
        {
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player.gameObject))
            {
                fighter.Attack(player.gameObject);
            }
            else
            {
                fighter.Cancel();
                GetComponent<Mover>().StartMoveAction(startPos);
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            var playerDistance = Vector3.Distance(transform.position, player.transform.position);
            return playerDistance <= chaseDistance;
        }
    }
}