using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.InventoryControl
{
    public class ContainerLink : MonoBehaviour
    {

        [SerializeField] UnityEvent<Inventory> openContainer;
        [SerializeField] UnityEvent closeContainer;


        public void OpenContainer(Container container)
        {
            Inventory inventory = container.GetComponent<Inventory>();
            openContainer.Invoke(inventory);
        }

        public void CloseContainer()
        {
            closeContainer.Invoke();
        }
    }
}
