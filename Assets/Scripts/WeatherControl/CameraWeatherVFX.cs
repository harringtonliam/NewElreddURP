using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.WeatherControl
{
    public class CameraWeatherVFX : MonoBehaviour
    {
        [SerializeField] WeatherVFX[] weatherVFXes;
        [SerializeField] WeatherContoller weatherContoller;
        [SerializeField] bool WeatherEffectsOn = true;
        [SerializeField] float interiorLoactionHeight = -50f;


        private float storedPositionY;

        [System.Serializable]
        public struct WeatherVFX
        {
            [SerializeField] public Weathers weather;
            [SerializeField]  public GameObject vfx;
        }

        // Start is called before the first frame update
        void Start()
        {
            storedPositionY = transform.position.y;

            if (weatherContoller != null)
            {
                weatherContoller.weatherHasChanged += SetVFX;
            }
            SetVFX();
        }

        private void Update()
        {
            if (Mathf.Abs(storedPositionY - transform.position.y) > 20)
            {
                storedPositionY = transform.position.y;
                SetVFX();
            }    
        }

        public void SetVFX()
        {
            foreach (var weatherVFX in weatherVFXes)
            {
                weatherVFX.vfx.SetActive(false);
            }
            if (!WeatherEffectsOn) return;
            if (IsInteriorLocation()) return;

            foreach (var weatherVFX in weatherVFXes)
            {
                if (weatherContoller.CurrentWeather == weatherVFX.weather)
                {
                    weatherVFX.vfx.SetActive(true);
                }
            }
        }

        private bool IsInteriorLocation()
        {
            if(transform.position.y <= interiorLoactionHeight)
            {
                return true;
            }
            return false;
        }
    }

}


