using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RPG.Saving;

namespace RPG.GameTime
{
    public class GameTimeContoller : MonoBehaviour, ISaveable
    {
        [SerializeField] float hourLenghtInRealMinutes = 10f;
        [SerializeField] int hoursInDay = 24;
        [SerializeField] int startHour = 10;
        [SerializeField] int startDayInMonth = 1;
        [SerializeField] int startYear = 579;
        [SerializeField] int daysInMonth = 28;
        [SerializeField] Months startMonth;
        [SerializeField] WeekDays[] weekDays;
        [SerializeField] Months[] months;
        [SerializeField] float timeUpdateIntervalInMinutes = 1f;

        public event Action timeUpdate;
        public event Action hourHasPassed;
        public event Action dayHasPassed;
        public event Action weekHasPassed;
        public event Action monthHasPassed;
        public event Action yearHasPassed;

        //Properties

        float timeSinceStartOfHour = 0f;
        float timeSinceTimeUpdate = 0f;

        private int currentYear;
        private int currentMonth;
        private int currentDayOfMonth;
        private int currentDayOfWeek;
        private int currentHour;

        public int CurrentYear {  get { return currentYear; } }
        public int CurrentMonth{ get { return currentMonth; } }
        public int CurrentDayOfMonth { get { return currentDayOfMonth; } }
        public int CurrentDayOfWeek { get { return currentDayOfWeek; } }
        public int CurrentHour { get { return currentHour; } }


        // Start is called before the first frame update
        void Start()
        {
            currentHour = startHour;
            currentDayOfWeek = 0;
            currentDayOfMonth = startDayInMonth;
            currentYear = startYear;
            currentMonth = GetStartMonth();

            TriggerAllEventActions();
            Debug.Log("GameTimeContoller  start " + CurrentYear + " " + GetCurrentMonth() + " " + CurrentDayOfMonth + " " + GetCurrentDayOfWeek() + " " + CurrentHour);
        }



        // Update is called once per frame
        void Update()
        {
            timeSinceStartOfHour += Time.deltaTime;
            timeSinceTimeUpdate += Time.deltaTime;
            CheckForTimeUpdate();
            CheckForStartOfHour();
        }

        public WeekDays GetCurrentDayOfWeek()
        {
            return weekDays[currentDayOfWeek];
        }

        public Months GetCurrentMonth()
        {
            return months[currentMonth];
        }

        public float GetHourExact()
        {
            float exactHour = currentHour;
            exactHour = currentHour + (timeSinceStartOfHour / 60 / hourLenghtInRealMinutes);
            return exactHour;
        }


        private void CheckForTimeUpdate()
        {
            if (((timeSinceTimeUpdate/60) > timeUpdateIntervalInMinutes)  && (timeUpdate!= null))
            {
                timeUpdate();
                timeSinceTimeUpdate = 0f;
            }
        }

        private void CheckForStartOfHour()
        {
            if ((timeSinceStartOfHour / 60) >= hourLenghtInRealMinutes)
            {
                currentHour++;
                CheckForNewDay();
                timeSinceStartOfHour = 0f;
                Debug.Log("GameTimeContoller New Hour Started " + CurrentYear + " " + GetCurrentMonth() + " " + CurrentDayOfMonth + " " + GetCurrentDayOfWeek() + " " + CurrentHour) ;
                if (hourHasPassed != null)
                {
                    hourHasPassed();
                }

            }
        }

        private void CheckForNewDay()
        {
            if (currentHour >= hoursInDay)
            {
                currentHour = 0;
                currentDayOfWeek++;
                CheckForNewWeek();
                currentDayOfMonth++;
                CheckForNewMonth();
                Debug.Log("GameTimeContoller New DayStarted Started " + currentHour);
                if (dayHasPassed != null)
                {
                    dayHasPassed();
                }

            }
        }

        private void CheckForNewWeek()
        {
            if (currentDayOfWeek > weekDays.Length)
            {
                currentDayOfWeek = 0;
                if (weekHasPassed != null)
                {
                    weekHasPassed();
                }

            }
        }

        private void CheckForNewMonth()
        {
            if (currentDayOfMonth > daysInMonth)
            {
                currentDayOfMonth = 1;
                currentMonth++;
                CheckForNewYear();
                if (monthHasPassed != null)
                {
                    monthHasPassed();
                }

            }
        }

        private void CheckForNewYear()
        {
            if (currentMonth > months.Length)
            {
                currentMonth = 0;
                currentYear++;
                if (yearHasPassed != null)
                {
                    yearHasPassed();
                }
            }
        }

        private void TriggerAllEventActions()
        {
            if (yearHasPassed != null)
            {
                yearHasPassed();
            }
            if (monthHasPassed != null)
            {
                monthHasPassed();
            }
            if (weekHasPassed != null)
            {
                weekHasPassed();
            }
            if (dayHasPassed != null)
            {
                dayHasPassed();
            }
            if (hourHasPassed != null)
            {
                hourHasPassed();
            }
            if (timeUpdate != null)
            {
                timeUpdate();
            }

        }

        private int GetStartMonth()
        {
            int i = 0;
            foreach (var month in months)
            {
                if (month == startMonth)
                {
                    return i;
                }
                i++;
            }

            return i;
        }

        [System.Serializable]
        private struct GameTimeStateData
        {
            public int currentYear;
            public int currentMonth;
            public int currentDayOfMonth;
            public int currentDayOfWeek;
            public int currentHour;
        }

        public object CaptureState()
        {

            GameTimeStateData gameTimeStateData = new GameTimeStateData();
            gameTimeStateData.currentDayOfMonth = currentDayOfMonth;
            gameTimeStateData.currentDayOfWeek = currentDayOfWeek;
            gameTimeStateData.currentHour = currentHour;
            gameTimeStateData.currentMonth = currentMonth;
            gameTimeStateData.currentYear = currentYear;
            return gameTimeStateData;
        }

        public void RestoreState(object state)
        {
            GameTimeStateData gameTimeStateData = (GameTimeStateData)state;
            currentDayOfMonth = gameTimeStateData.currentDayOfMonth ;
            currentDayOfWeek = gameTimeStateData.currentDayOfWeek;
            currentHour = gameTimeStateData.currentHour;
            currentMonth = gameTimeStateData.currentMonth;
            currentYear = gameTimeStateData.currentYear;
            TriggerAllEventActions();
        }
    }


}






