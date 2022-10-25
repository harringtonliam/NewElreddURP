using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI.CharacterGenerator
{
    public class AbilityScoreDisplayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI abilityScoreTextText = null;
        [SerializeField] CharacterAbilitySlotUI characterAbilitySlotUI = null;
        [SerializeField] TextMeshProUGUI abilityScoreBonusText = null;
        [SerializeField] int index = 0;

        public void Setup(string abilityDesc, int abilityScoreValue, int index)
        {
            abilityScoreTextText.text = abilityDesc;
            this.index = index;
            characterAbilitySlotUI.SetUp(index, abilityScoreValue);
        }

    }

}


