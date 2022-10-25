using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;

namespace RPG.UI.SceneManagement
{


    public class Menu : MonoBehaviour
    {
        [SerializeField] int startSceneIndex = 1;
        [SerializeField] GameObject loadGameCanvas = null;
 

        public void LoadStartScenee()
        {
            FindObjectOfType<SavingWrapper>().DeleteDefaultSaveFile();
            SceneManager.LoadScene(startSceneIndex);
        }

        public void Quit()
        {
            Application.Quit();   
        }

        public void ShowLoadGameCanvas()
        {
            if (loadGameCanvas == null) return;

            loadGameCanvas.SetActive(true);
            
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }



    }

}



