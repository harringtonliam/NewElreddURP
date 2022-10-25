using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;

namespace RPG.UI.CharacterGenerator
{

    public class AssignedAbilityScoresUI : MonoBehaviour
    {
        [SerializeField] AbilityScoreDisplayUI abilityScoreDisplayPrefab;

        CharacterAbilities characterAbilities;

        private void Awake()
        {
            characterAbilities = FindObjectOfType<CharacterAbilities>();
            characterAbilities.characterAbilitiesUpdated += Redraw;
        }

        // Start is called before the first frame update
        void Start()
        {

            Redraw();
        }

        
        public void Redraw()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }


            for (int i = 0; i < characterAbilities.GetCharacterAbilities().Length; i++)
            {
                var abilityScoreDisplay = Instantiate(abilityScoreDisplayPrefab, transform);
                abilityScoreDisplay.Setup(characterAbilities.GetCharacterAbilities()[i].Ability.ToString(), characterAbilities.GetCharacterAbilities()[i].AbilityValue, i);
            }


        }

    }
}


