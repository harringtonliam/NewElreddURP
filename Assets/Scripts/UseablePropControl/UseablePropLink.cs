using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.UseablePropControl
{

    public class UseablePropLink : MonoBehaviour
    {

        public event Action onDisplayUseablePropUI;


        UseableProp currentUseableProp;

        public UseableProp CurrentUsableProp
        {
            get { return currentUseableProp; }
        }

        public void DisplayUsePropUI(UseableProp useableProp)
        {
            currentUseableProp = useableProp;
            if (onDisplayUseablePropUI != null)
            {
                onDisplayUseablePropUI();
            }
        }


    }

}
