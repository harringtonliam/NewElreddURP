using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RPG.UI.DamageText
{

    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;



        public void Spawn(float damage)
        {
            if (damageTextPrefab == null) return;

            
            DamageText damageTextInstance = Instantiate<DamageText>(damageTextPrefab, transform);
            damageTextInstance.SetValue(damage);

        }
    }


}


