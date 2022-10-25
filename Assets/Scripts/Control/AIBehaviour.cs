using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.GameTime;
using System;
using System.Linq;

namespace RPG.Control
{
    public class AIBehaviour : MonoBehaviour
    {
        [SerializeField] BehaviourDescription[] behaviourDescriptions;

        [System.Serializable]
        public class BehaviourDescription
        {
            public bool appliesToAllMonths = true;
            public Months month;
            public bool appliesToSpecificWeekDay = false;
            public WeekDays weekDay;
            public bool appliesToAllDays = true;
            public int dayFrom;
            public int dayTo;
            public int hourFrom;
            public int hourTo;
            public PatrolPath patrolPath;
            public float waypointPauseTime = 2f;
            public float patrolSpeedFraction = 0.2f;

            public string Print()
            {
                return "Behaviour Description applies to all months " + appliesToAllMonths + " month  " + month + " appliestospecfic weekday " + appliesToSpecificWeekDay + ": " + weekDay +"appplies to all days " + appliesToAllDays + " day from and to " + dayFrom + " " + dayTo + " hours from and to " + hourFrom + " " + hourTo;
            }
        }

        AIControler aIControler;
        GameTimeContoller gameTimeContoller;

        // Start is called before the first frame update
        void Start()
        {
            aIControler = GetComponent<AIControler>();
            gameTimeContoller = FindObjectOfType<GameTimeContoller>();
            gameTimeContoller.hourHasPassed += CheckBehaviour;
        }


        private void CheckBehaviour()
        {
            var sortedBehaviours = behaviourDescriptions.OrderBy(m => m.appliesToAllMonths).ThenByDescending(w => w.appliesToSpecificWeekDay).ThenBy(d => d.appliesToAllDays).ToArray();
            for (int i = 0; i < sortedBehaviours.Length; i++)
            {
                //Debug.Log("Checking Behaviour sorted array " + i + " " + sortedBehaviours[i].Print()); 
                if (BehaviourApplies(sortedBehaviours[i]))
                {
                    ApplyBehaviour(sortedBehaviours[i]);
                    return;
                }
            }
            ApplyNoBehaviour();
        }

        private void ApplyBehaviour(BehaviourDescription behaviourDescription)
        {
            aIControler.SetPatrolPath(behaviourDescription.patrolPath, true);
            aIControler.SetPatrolSpeedFraction(behaviourDescription.patrolSpeedFraction);
            aIControler.SetWayPointPauseTime(behaviourDescription.waypointPauseTime);
        }

        private void ApplyNoBehaviour()
        {
            aIControler.SetPatrolPath(null, false);
        }

        private bool BehaviourApplies(BehaviourDescription behaviourDescription)
        {
            bool useThisBehavior = true;

            if(!behaviourDescription.appliesToAllMonths && behaviourDescription.month != gameTimeContoller.GetCurrentMonth())
            {
                useThisBehavior = false;
            }
            if (behaviourDescription.appliesToSpecificWeekDay && behaviourDescription.weekDay != gameTimeContoller.GetCurrentDayOfWeek())
            {
                //Debug.Log("Failed weekday check " + behaviourDescription.appliesToSpecificWeekDay + " " + behaviourDescription.weekDay + " " + gameTimeContoller.GetCurrentDayOfWeek());
                useThisBehavior = false;
            }
            if (!behaviourDescription.appliesToAllDays && (gameTimeContoller.CurrentDayOfMonth  < behaviourDescription.dayFrom || gameTimeContoller.CurrentDayOfMonth > behaviourDescription.dayTo))
            {
                //Debug.Log("Failed day check " + behaviourDescription.appliesToAllDays + " " + behaviourDescription.dayFrom + " " + behaviourDescription.dayTo + " " + gameTimeContoller.CurrentDayOfMonth);
                useThisBehavior = false;
            }
            if (gameTimeContoller.CurrentHour < behaviourDescription.hourFrom  || gameTimeContoller.CurrentHour > behaviourDescription.hourTo)
            {
                //Debug.Log("Failed hour check "  + " " + behaviourDescription.hourFrom + " " + behaviourDescription.hourTo + " " + gameTimeContoller.CurrentHour);
                useThisBehavior = false;
            }

            return useThisBehavior;
        }
    }
}

