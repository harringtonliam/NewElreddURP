using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace RPG.Movement
{
    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] TextMeshPro tmproText;

        private GridObject gridObject;

        public void SetGridDebugObject(GridObject gridObject)
        {
            this.gridObject = gridObject;
        }

        private void Update()
        {
            tmproText.text = gridObject.ToString();
        }

    }

}



