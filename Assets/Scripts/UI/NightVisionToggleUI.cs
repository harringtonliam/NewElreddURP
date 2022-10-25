using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core;

namespace RPG.UI
{
    public class NightVisionToggleUI : MonoBehaviour
    {
        [SerializeField] Image buttonImage;

        InGameSettings inGameSettings;


        private void Start()
        {
            inGameSettings = FindObjectOfType<InGameSettings>();
        }

        public void ButtonClicked()
        {
            inGameSettings.SetIsNighttVisionOn(!inGameSettings.IsNightVisionOn);

            if (inGameSettings.IsNightVisionOn)
            {
                buttonImage.color = Color.red;
            }
            else
            {
                buttonImage.color = Color.white;
            }
        }


    }

}


