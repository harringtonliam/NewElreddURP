using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;

namespace RPG.UI.Information
{
    public class AbilitiesListUI : MonoBehaviour
    {

        [SerializeField] AbilityUI abilitytUIPrefab = null;

        CharacterAbilities characterAbilities;
        GameObject player = null;

        // Start is called before the first frame update
        void Start()
        {

            player = GameObject.FindGameObjectWithTag("Player");
            characterAbilities = player.GetComponent<CharacterAbilities>(); 

            RedrawUI();
        }


        private void RedrawUI()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var ability in characterAbilities.GetCharacterAbilities())
            {
                var abilityUI = Instantiate(abilitytUIPrefab, transform);
                abilityUI.SetUp(ability, characterAbilities.GetAbilityModifier(ability.Ability));
            }

        }
            
    }
}


