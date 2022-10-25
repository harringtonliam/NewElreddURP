using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighting fighter;

        void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            fighter = player.GetComponent<Fighting>();
        }

        private void Update()
        {
            if (fighter.GetTarget() != null)
            {
                Health health = fighter.GetTarget();
                GetComponent<Text>().text = health.HealthPoints.ToString() + "/" + health.GetMaxHealthPoints();
            }
            else
            {
                GetComponent<Text>().text = "NA";
            }

        }
    }

}
