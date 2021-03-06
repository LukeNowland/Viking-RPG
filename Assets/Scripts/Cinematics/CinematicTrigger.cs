using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && !isTriggered)
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
        }
    }
}