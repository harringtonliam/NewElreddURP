using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.WeatherControl
{
    [System.Serializable]
    public class WeatherEffect 
    {
        [SerializeField] Weathers weather;
        [SerializeField] float lightIntensityPercentage;
        [SerializeField] float lightShadowStrenght;
        public Weathers Weather { get { return weather; } }
        public float LightIntesityPercentage { get { return lightIntensityPercentage; } }
        public float LightShadowStrenght { get { return lightShadowStrenght; } }

    }



}


