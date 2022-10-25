using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Menus
{
    public class PlayPause : MonoBehaviour
    {
        [SerializeField] Sprite playImage;
        [SerializeField] Sprite pauseImage;
        [SerializeField] Image buttonImage;
        [SerializeField] KeyCode toogleKey = KeyCode.P;




        private void Update()
        {
            if(Input.GetKeyDown(toogleKey))
            {
                ButtonClicked();
            }
        }

        public void ButtonClicked()
        {

            if (Mathf.Approximately(Time.timeScale, 0))
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }


        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            buttonImage.sprite = playImage;
        }


        public void ResumeGame()
        {
            Time.timeScale = 1;
            buttonImage.sprite = pauseImage;
        }
    }

}



