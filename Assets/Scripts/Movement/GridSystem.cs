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
        private GridObject[,] gridObjects;

        public GridSystem(int width, int length, float cellSize)
        {
            this.width = width;
            this.length = length;
            this.cellSize = cellSize;

            gridObjects = new GridObject[width, length];

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    gridObjects[x,z] = new GridObject(this, gridPosition);
                }
            }
        }


        public  Vector3 GetWorldPosition(GridPosition gridPostion)
        {
            return new Vector3(gridPostion.x, 0, gridPostion.z) * cellSize;
        }

        public GridPosition GetGridPosition(Vector3 position)
        {
            return new GridPosition(Mathf.RoundToInt(position.x / cellSize), Mathf.RoundToInt(position.z / cellSize));
        }

        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < length; z++)
                {
                    GridPosition gridPostion = new GridPosition(x, z);

                    Transform gridDebugObject =  GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPostion), Quaternion.identity);
                    gridDebugObject.GetComponent<GridDebugObject>().SetGridDebugObject(GetGridObject(gridPostion));
                }
            }
        }

        public GridObject GetGridObject(GridPosition gridPostion)
        {
            return gridObjects[gridPostion.x, gridPostion.z];
        }

        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            if (gridPosition.x >= 0 && gridPosition.z >= 0  && gridPosition.z < width && gridPosition.z < length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetLength()
        {
            return length;
        }

    }

}


