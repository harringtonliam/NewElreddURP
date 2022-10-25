using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.WeatherControl
{
    [CreateAssetMenu(fileName = "WeatherDescriptions", menuName = "Weather/New Weather Descriptions", order = 1)]
    public class WeatherDescriptions : ScriptableObject
    {
        [SerializeField] WeatherEffect[] weatherEffects = null;

        public WeatherEffect GetWeatherEffect(Weathers weather)
        {
            foreach (var weatherEffect in weatherEffects)
            {
                if (weatherEffect.Weather == weather)
                {
                    return weatherEffect;
                }
            }
            return null;
        }
    }


}

