using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;

namespace RPG.Stats
{ 

public class ExperienceDisplay : MonoBehaviour
{
        Experience experience;

        void Awake()
        {
            experience = PlayerSelector.GetFirstSelectedPlayer().GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<Text>().text = experience.ExperiencePoints.ToString();
        }
    }

}
