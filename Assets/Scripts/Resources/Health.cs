using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float percentageOfMax = 50f;

        float health = -1f;

        private bool isDead = false;

        private void Awake()
        {
            GetComponent<BaseStats>().onLevelUp += IncreaseHealth;
            if (health < 0)
            {
                health = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void IncreaseHealth()
        {
            var maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);

            var newHealth = health + ((percentageOfMax / 100) * maxHealth);

            if (newHealth >= maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health = newHealth;
            }

            print("Health increased");
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);

            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                AwardXP(instigator);
                HandleDeath();
            }
        }

        private void AwardXP(GameObject instigator)
        {
            var XP = instigator.GetComponent<Experience>();
            if (XP == null) return;
            XP.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100f * (health / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void HandleDeath()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if (health == 0)
            {
                HandleDeath();
            }
        }
    }
}
