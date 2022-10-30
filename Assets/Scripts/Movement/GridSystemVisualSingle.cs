using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Movement
{
    public class GridSystemVisualSingle : MonoBehaviour
    {
        [SerializeReference] private MeshRenderer meshRenderer;

        public void Show()
        {
            meshRenderer.enabled = true;
        }

        public void Hide()
        {
            meshRenderer.enabled = false; ;
        }
    }

}

