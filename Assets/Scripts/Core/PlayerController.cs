using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool hasHit = Physics.Raycast(ray, out hitInfo);
                if (hasHit)
                {
                    GetComponent<Mover>().MoveTo(hitInfo.point);
                }
            }
        }
    }
}