using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Core;

namespace RPG.Combat
{
    public class Trap : MonoBehaviour
    {

        [SerializeField] int damgeDice = 10;
        [SerializeField] int damageDiceNumber = 4;
        [SerializeField] int damageAdditiveBonus = 0;
        [SerializeField] float triggerFXDestroyTime = 5f;
        [SerializeField] bool isActive = true;
        [SerializeField] GameObject triggerFX;
        [SerializeField] GameObject standardFx;

        const string playerTag = "Player";

        private void Start()
        {
            ActivateFX();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!isActive) return;

            Health health = other.GetComponent<Health>();
            GameObject player = GameObject.FindWithTag(playerTag);
            GameObject instigator;


            if (health == null) return;

            float calcDamage = FindObjectOfType<Dice>().RollDice(damgeDice, damageDiceNumber) + damageAdditiveBonus;

            if (other.tag == playerTag)
            {
                instigator = gameObject;
            }
            else
            {
                instigator = player;
            }

            health.TakeDamage(calcDamage, instigator);
            if (triggerFX != null)
            {
                GameObject fx= GameObject.Instantiate(triggerFX, other.transform.position, Quaternion.identity, gameObject.transform);
                Destroy(fx, triggerFXDestroyTime);
            }

        }

        public void Activate(bool activate)
        {
            isActive = activate;
            ActivateFX();
        }

        private void ActivateFX()
        {
            if (standardFx != null)
            {
                standardFx.SetActive(isActive);
            }
        }
    }

}



