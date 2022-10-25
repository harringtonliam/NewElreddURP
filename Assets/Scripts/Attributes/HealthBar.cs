using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Health healthComponent = null;
        [SerializeField] Canvas rootCanvas = null;

        void Update()
        {
            if (Mathf.Approximately(GetHealthFraction(), 0) || Mathf.Approximately(GetHealthFraction(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;

            foreground.localScale = new Vector3(healthComponent.HealthPoints / healthComponent.GetMaxHealthPoints(),1,1);

        }

        private float GetHealthFraction()
        {
            if (healthComponent == null) return 1f;

            return healthComponent.HealthPoints / healthComponent.GetMaxHealthPoints();
        }
    }
}


