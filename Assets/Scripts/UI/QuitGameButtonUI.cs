using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.UI
{
    public class QuitGameButtonUI : MonoBehaviour
    {
        public void QuitGame()
        {
            SceneManager.LoadScene(0);
        }

    }

}


