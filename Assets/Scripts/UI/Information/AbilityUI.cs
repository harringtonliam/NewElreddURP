using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Stats;

namespace RPG.UI.Information
{
    public class AbilityUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI abilityText;
        [SerializeField] TextMeshProUGUI abilityScore;
        [SerializeField] TextMeshProUGUI abilityBonus;

        public void SetUp(CharacterAbility characterAbility, int abilityModifier)
        {
            abilityText.text = characterAbility.Ability.ToString();
            abilityScore.text = characterAbility.AbilityValue.ToString();
            if (abilityModifier >=0)
            {
                abilityBonus.text = "+" + abilityModifier.ToString();
            }
            else
            {
                abilityBonus.text = "-" + abilityModifier.ToString();
            }

        }
    }

}

