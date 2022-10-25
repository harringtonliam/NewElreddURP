using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.SceneManagement;
using TMPro;
using RPG.Core;

namespace RPG.UI.Menus
{
    public class SaveGameUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI savedGameNameText = null;
        [SerializeField] TextMeshProUGUI savedGameTimeText = null;





        public void Setup(string savedGameName, string savedGameTime)
        {

            savedGameNameText.text = savedGameName;
            savedGameTimeText.text = savedGameTime;
        }


        public void SaveGame()
        {
            FindObjectOfType<SavingWrapper>().Save(savedGameNameText.text);

        }

        public void DeleteGame()
        {
            FindObjectOfType<SavingWrapper>().Delete(savedGameNameText.text);
        }

        public void LoadGame()
        {
            FindObjectOfType<SavingWrapper>().LoadSavedGame(savedGameNameText.text);

        }


    }

}


