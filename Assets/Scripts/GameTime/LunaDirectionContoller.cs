using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameTime
{
    public class LunaDirectionContoller : MonoBehaviour
    {

        [SerializeField] Light lunaDirectionalLight;
        [SerializeField] float rotationMultiplier = 12.85f;
        [SerializeField] float rotationOffset = -90f;
        [SerializeField] float maxRotation = 359f;

        GameTimeContoller gameTimeContoller;

        // Start is called before the first frame update
        void Start()
        {
            gameTimeContoller = GetComponent<GameTimeContoller>();
            gameTimeContoller.dayHasPassed += CalculateLunaDirection;
        }


        private void CalculateLunaDirection()
        {
            if (lunaDirectionalLight == null) return;

            float newXRotation = (gameTimeContoller.CurrentDayOfMonth * rotationMultiplier) + rotationOffset;
            if (newXRotation >= maxRotation)
            {
                newXRotation = 0f;
            }

            Vector3 sunRotation = new Vector3(newXRotation, 0f, 0f);

            lunaDirectionalLight.transform.eulerAngles = sunRotation;
            Debug.Log("LunaDirectionContoller new x rotation " + lunaDirectionalLight.transform.rotation.x.ToString());

        }
    }

}


