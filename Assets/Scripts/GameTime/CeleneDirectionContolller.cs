using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameTime
{
    public class CeleneDirectionContolller : MonoBehaviour
    {
        [SerializeField] Light celeneDirectionalLight;
        [SerializeField] float rotationMultiplier = 12.85f;
        [SerializeField] float rotationOffset = -90f;
        [SerializeField] float maxRotation = 359f;

        GameTimeContoller gameTimeContoller;

        // Start is called before the first frame update
        void Start()
        {
            gameTimeContoller = GetComponent<GameTimeContoller>();
            gameTimeContoller.monthHasPassed += CalculateCeleneDirection;
        }


        private void CalculateCeleneDirection()
        {
            if (celeneDirectionalLight == null) return;

            float newXRotation = (gameTimeContoller.CurrentMonth * rotationMultiplier) + rotationOffset;
            if (newXRotation >= maxRotation)
            {
                newXRotation = 0f;
            }

            Vector3 sunRotation = new Vector3(newXRotation, 0f, 0f);

            celeneDirectionalLight.transform.eulerAngles = sunRotation;


        }
    }
}


