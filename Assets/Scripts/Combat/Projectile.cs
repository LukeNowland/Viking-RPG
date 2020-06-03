using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float speed = 5f;

    void Update()
    {
        if (target == null) return;
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private Vector3 GetAimLocation()
    {
        var targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.position;
        return target.position + Vector3.up * (targetCapsule.height/2);
    }
}
