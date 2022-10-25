using UnityEngine;
using RPG.Core;
using RPG.Attributes;
using RPG.Stats;
using RPG.InventoryControl;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : EquipableItem
    {
        [SerializeField] AnimatorOverrideController weaponOverrideController = null;
        [SerializeField] Weapon equipedPrefab = null;
        [SerializeField] AmmunitionType ammunitionType = AmmunitionType.None;
        [SerializeField] int weaponDamageDice = 4;
        [SerializeField] int weaponDamageDiceNumber = 1;
        [SerializeField] int weaponDamageAdditiveBonus = 0;
        [SerializeField] int weaponToHitBonus = 0;
        [SerializeField] float percentageBonus = 0f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] bool isRangedWeapon = false;
        [SerializeField] Projectile projectile = null;
        [SerializeField] Ability modifierAbility = Ability.Strength;


        const string weaponName = "Weapon";

        public AmmunitionType AmmunitionType {  get { return ammunitionType; } }

        public float WeaponDamage
        {
            get { return weaponDamageDice; }
        }

        public int WeaponDamageDiceNumber
        {
            get { return weaponDamageDiceNumber; }
        }

        public float PercentageBonus
        {
            get { return percentageBonus; }
        }

        public float WeaponRange
        {
            get { return weaponRange; }
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public int WeaponToHitBonus
        {
            get { return weaponToHitBonus; }
        }

        public int WeaponDamageAdditiveBonus
        {
            get { return weaponDamageAdditiveBonus; }
        }

        public Ability ModifierAbility
        {
            get { return modifierAbility; }
        }

        public bool IsRangedWeapon
        {
            get { return isRangedWeapon; }
        }

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            Weapon weapon = null;

            if (equipedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                weapon = Instantiate(equipedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }
            if (weaponOverrideController != null)
            {
                animator.runtimeAnimatorController = weaponOverrideController;
            }

            return weapon;
            
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon ==null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null)
            {
                return;
            }
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }

            return handTransform;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, calculatedDamage, instigator);
        }

        public int CalcWeaponDamage()
        {
            Dice dice = FindObjectOfType<Dice>();
            int calculatedDamage = Mathf.Clamp(dice.RollDice(weaponDamageDice, weaponDamageDiceNumber) + weaponDamageAdditiveBonus, 1, 100) ;
            return calculatedDamage;
        }
            
    }


}
