using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RPG.Control;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        void Awake()
        {
            health = PlayerController.GetFirstSelectedPlayer().GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<Text>().text =  health.HealthPoints.ToString() + "/" + health.GetMaxHealthPoints();
        }
    }

}
