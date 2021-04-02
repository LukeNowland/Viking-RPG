using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {  
            if (fighter.GetTarget() != null)
            {
                GetComponent<Text>().text = String.Format("{0}/{1}", fighter.GetTarget().GetHealth(),fighter.GetTarget().GetMaxHealth());
            }
            else
            {
                GetComponent<Text>().text = "N/A";
            }
        }
    }
}