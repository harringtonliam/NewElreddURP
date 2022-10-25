using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Attributes;
using TMPro;

namespace RPG.UI.InGame
{
    public class PlayerCharacterUI : MonoBehaviour
    {
        [SerializeField] RectTransform foregroundHeealthBar = null;
        [SerializeField] TextMeshProUGUI nameText = null;
        [SerializeField] TextMeshProUGUI rankText = null;
        [SerializeField] TextMeshProUGUI currentHPText = null;
        [SerializeField] TextMeshProUGUI maxHPText = null;
        [SerializeField] Image portraitImage = null;

        GameObject playerCharacterGameObject = null;
        Health health = null;


        string characterName = null;


        public void SetUp(GameObject newPlayerCharacterGameObject)
        {
            var iconImage = GetComponent<Image>();
            playerCharacterGameObject = newPlayerCharacterGameObject;

            CharacterSheet characterSheet = playerCharacterGameObject.GetComponent<CharacterSheet>();
            if (characterSheet != null)
            {
                portraitImage.sprite = characterSheet.Portrait;
                iconImage.enabled = true;
                characterName = characterSheet.CharacterName;
                nameText.text = characterName;
                if (rankText != null)
                {
                    rankText.text = characterSheet.Rank;
                }
            }

            health = playerCharacterGameObject.GetComponent<Health>();
            health.healthUpdated += UpdateHealth;
            UpdateHealth();

        }

        private void SetHealthText()
        {
            if (health == null) return;
            if (currentHPText != null)
            {
                currentHPText.text = health.HealthPoints.ToString();
            }
            if (maxHPText != null)
            {
                maxHPText.text = "/" + health.GetMaxHealthPoints().ToString();
            }
            SetHelthPointTextColor();
        }

        private void SetHelthPointTextColor()
        {
            if (health.HealthPoints < (health.GetMaxHealthPoints() * 0.33f))
            {
                currentHPText.faceColor = Color.red;
            }
            else if (health.HealthPoints < (health.GetMaxHealthPoints() * 0.66f))
            {
                currentHPText.faceColor = Color.yellow;
            }
            else if (health.HealthPoints < (health.GetMaxHealthPoints()))
            {
                currentHPText.faceColor = Color.green;
            }
            else
            {
                currentHPText.faceColor = Color.white;
            }
        }

        private void UpdateHealth()
        {
            if (health == null) return;
            SetHealthText();
            if (foregroundHeealthBar == null) return;
            Vector3 newScale = new Vector3(health.HealthPoints / health.GetMaxHealthPoints(), 1, 1);
            foregroundHeealthBar.localScale = newScale;
        }

        private float GetHealthFraction()
        {
            return health.HealthPoints / health.GetMaxHealthPoints();
        }
    }



}

