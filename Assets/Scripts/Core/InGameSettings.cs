using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RPG.Core
{
    public class InGameSettings : MonoBehaviour
    {

        bool isNightVisionOn = false;

        public event Action settingsUpdated;


        public bool IsNightVisionOn
        {
            get { return isNightVisionOn; }
        }

        public void SetIsNighttVisionOn(bool isNightVisionOn)
        {
            this.isNightVisionOn = isNightVisionOn;
            if (settingsUpdated != null)
            {
                settingsUpdated();
            }

        }

    }
}


