using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameTime
{
    public class LightContoller : MonoBehaviour
    {
        [SerializeField] LightOnTimeRange[] lightOnTimeRanges;
        [SerializeField] Light[] lights;

        [System.Serializable]
        public struct LightOnTimeRange {
            [SerializeField] public int lightOnStartHour;
            [SerializeField] public int inligtOnEndHour;
        }

        GameTimeContoller gameTimeContoller;
        float[] startLightIntenties;

        // Start is called before the first frame update
        void Start()
        {
            gameTimeContoller = FindObjectOfType<GameTimeContoller>();
            gameTimeContoller.hourHasPassed += LightsOnCheck;
            StoreInitialLightIntensities();
        }

        private void StoreInitialLightIntensities()
        {
            startLightIntenties = new float[lights.Length];
            for (int i = 0; i < lights.Length; i++)
            {
                startLightIntenties[i] = lights[i].intensity;
            }
        }

        private void LightsOnCheck()
        {
            TurnOffLights();
            foreach (var lightOnTimeRange in lightOnTimeRanges)
            {
                if (IsInLightOnRange(gameTimeContoller.CurrentHour, lightOnTimeRange))
                {
                    TurnOnLights();
                }
            }
        }

        private bool IsInLightOnRange(int currentHour, LightOnTimeRange lightOnTimeRange)
        {
            if (currentHour >= lightOnTimeRange.lightOnStartHour && currentHour <= lightOnTimeRange.inligtOnEndHour)
            {
                return true;
            }
            return false;
        }

        private void TurnOffLights()
        {
            foreach (var light in lights)
            {
                light.intensity = 0f;
            }
        }

        private void TurnOnLights()
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].intensity = startLightIntenties[i];
            }
        }


    }

}


