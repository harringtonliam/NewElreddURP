using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI.CharacterGenerator
{
    public class RolledScoreUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreTextBox = null;


        public void SetUp(int score)
        {
            scoreTextBox.text = score.ToString();
        }

        
    }
}


