using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] GameObject[] destroyOnHit = null;

        Health target = null;
        GameObject instigator = null;
        float damage = 0;
        float maxLifeTime = 3f;

        private void Start()
        {
            if (target == null) return;
            if (!isHoming) transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            var targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) return target.transform.position;
            return target.transform.position + Vector3.up * (targetCapsule.height / 2);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(instigator, damage);
            speed = 0;

            if (hitEffect != null)
            {
                GameObject newImpact = Instantiate(hitEffect, GetAimLocation(), transform.rotation) as GameObject;
            }

            DestroyProjectile();
        }

        private void DestroyProjectile()
        {
            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }
        }
    }
}