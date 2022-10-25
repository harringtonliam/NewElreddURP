using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI.CharacterGenerator
{
    public class AbilityScoreValueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI abilityScoreValueText = null;

        public void SetValue(int value)
        {

            var iconImage = GetComponent<Image>();
            if (value < 1)
            {
                iconImage.enabled = false;
                abilityScoreValueText.text = string.Empty;
            }
            else
            {
                iconImage.enabled = true;
                abilityScoreValueText.text = value.ToString();
            }
            
        }

        public string GetValue()
        {
            return abilityScoreValueText.text;
        }
    }

}


