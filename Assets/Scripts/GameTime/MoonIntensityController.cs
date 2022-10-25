using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameTime
{
    public class MoonIntensityController : MonoBehaviour
    {
        [SerializeField] GameTimeContoller gameTimeContoller;
        [SerializeField] Light moon;
        [SerializeField] Months[] monthsVisible;
        [SerializeField] int dayVisibleFrom = 10;
        [SerializeField] int dayVisibleTo = 18;
        [SerializeField] float maxIntensity = 0.1f;


        // Start is called before the first frame update
        void Start()
        {
            gameTimeContoller.dayHasPassed += SetMoonIntensity;
        }

        private void SetMoonIntensity()
        {
            if (IsVisibleThisMonth() && IsVisibleThisDay())
            {
                moon.intensity = maxIntensity;
            }
            else
            {
                moon.intensity = 0f;
            }
        }


        private bool IsVisibleThisMonth()
        {
            if (gameTimeContoller == null) return false;
            if (moon == null) return false;
            Months currentMonth = gameTimeContoller.GetCurrentMonth();
            foreach (var month in monthsVisible)
            {
                if (currentMonth == month)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsVisibleThisDay()
        {
            if (gameTimeContoller.CurrentDayOfMonth >= dayVisibleFrom && gameTimeContoller.CurrentDayOfMonth <= dayVisibleTo)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


