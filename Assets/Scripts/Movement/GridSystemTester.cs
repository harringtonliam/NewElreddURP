using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{


    public class GridSystemTester : MonoBehaviour
    {
        [SerializeField] Transform gridDebugObjectPrefab;


        private GridSystem gridSystem;
        
        // Start is called before the first frame update
        void Start()
        {
            gridSystem = new GridSystem(100, 100, 1.5f);
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }





    }
}
