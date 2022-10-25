using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.SceneManagement;


namespace RPG.UI.Menus
{
    public class NewSaveGameUI : MonoBehaviour
    {
        [SerializeField] InputField savedGameNameInput = null;

        public void SaveGame()
        {
            FindObjectOfType<SavingWrapper>().Save(savedGameNameInput.text);
            savedGameNameInput.text = string.Empty;

        }


    }

}


