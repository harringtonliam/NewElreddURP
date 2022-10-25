using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameTime
{
    public class SunDirectionController : MonoBehaviour
    {
        [SerializeField] Light sunDirectionalLight;
        [SerializeField] float sunRotationMultiplier = 15f;
        [SerializeField] float sunRotationOffset = -90f;
        [SerializeField] float maxRotation = 359f;
        [SerializeField] float zRotation = -20f;

        GameTimeContoller gameTimeContoller;

        // Start is called before the first frame update
        void Start()
        {
            gameTimeContoller = GetComponent<GameTimeContoller>();
            gameTimeContoller.timeUpdate += CalculateSunDirection;
        }


        private void CalculateSunDirection()
        {
            if (sunDirectionalLight == null) return;
            float newXRotation = (gameTimeContoller.GetHourExact() * sunRotationMultiplier) + sunRotationOffset;
            if (newXRotation >= maxRotation)
            {
                newXRotation = 0f;
            }
            Vector3 sunRotation = new Vector3(newXRotation, 0f, zRotation);
            sunDirectionalLight.transform.eulerAngles = sunRotation;
        }

    }
}


