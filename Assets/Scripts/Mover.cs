using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        var hasHit = Physics.Raycast(ray, out hitInfo);
        if (hasHit)
        {
        GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }
    }
}
