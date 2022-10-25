using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.InventoryControl;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField] WeaponConfig weaponPrefab;

        public WeaponConfig WeaponPrefab {  get { return weaponPrefab; } }

        private void OnTriggerEnter(Collider other)
        {
            GameObject otherGameObject = other.gameObject;

            if (otherGameObject.tag == "Player")
            {
                Fighting fighter = otherGameObject.GetComponent<Fighting>();
                Pickup(fighter);
            }
        }

        public void Pickup(Fighting fighter)
        {
            fighter.EquipWeapon(weaponPrefab);
            Destroy(gameObject);
        }

        public RaycastableReturnValue HandleRaycast(PlayerController playerController)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    PickupRetriever pickupRetriever = playerController.transform.GetComponent<PickupRetriever>();
            //    if (pickupRetriever != null)
            //    {
            //        pickupRetriever.StartPickupRetrieval(gameObject);
            //    }
            //}
            return RaycastableReturnValue.FirstPlayerCharacter;
        }

        public void HandleActivation(PlayerController playerController)
        {
            PickupRetriever pickupRetriever = playerController.transform.GetComponent<PickupRetriever>();
            if (pickupRetriever != null)
            {
                pickupRetriever.StartPickupRetrieval(gameObject);
            }
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }

}
