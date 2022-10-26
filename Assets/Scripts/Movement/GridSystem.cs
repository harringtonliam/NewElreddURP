using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
    public class GridSystem
    {
        private int width;
        private int length;
        private float cellSize;

        public GridSystem(int width, int length, float cellSize)
        {
            this.width = width;
            this.length = length;
            this.cellSize = cellSize;

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    Debug.DrawLine(GetWorldPosition(x,z), GetWorldPosition(x,z) + Vector3.right * 2f, Color.white, 1000);
                }
            }
        }


        public  Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0, z) * cellSize;
        }

        public GridPostion GetGridPosition(Vector3 position)
        {
            return new GridPostion(Mathf.RoundToInt(position.x / cellSize), Mathf.RoundToInt(position.z / cellSize));
        }
    }

}


