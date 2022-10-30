using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
    public class GridSystemVisual : MonoBehaviour
    {
        [SerializeReference] private Transform gridSystemVisualSinglePrefab;

        private GridSystemVisualSingle[,] gridSystemVisualSingles;

        private void Start()
        {
            gridSystemVisualSingles = new GridSystemVisualSingle[LevelGrid.Instance.GetWith(), LevelGrid.Instance.GetLength()];

            for (int x = 0; x < LevelGrid.Instance.GetWith(); x++)
            {
                for (int z = 0; z < LevelGrid.Instance.GetLength(); z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform gridSystemVisualSingle = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetGridWorld(gridPosition), Quaternion.identity);
                    gridSystemVisualSingles[x, z] = gridSystemVisualSingle.GetComponent<GridSystemVisualSingle>();
                }
            }
        }


        public void HideAllGridPositions()
        {
            try
            {
                for (int x = 0; x < gridSystemVisualSingles.GetUpperBound(0); x++)
                {
                    for (int z = 0; z < gridSystemVisualSingles.GetUpperBound(1); z++)
                    {
                        gridSystemVisualSingles[x, z].Hide();
                    }
                }
            }
            catch (System.Exception)
            {
                Debug.Log("HideAllGridPositions exception");
            }

        }

        public void ShowGridPositionList(List<GridPosition> gridPositionList)
        {
            try
            {
                foreach (var gridPosition in gridPositionList)
                    gridSystemVisualSingles[gridPosition.x, gridPosition.z].Show();

            }
            catch (System.Exception)
            {
                                Debug.Log("ShowGridPositionList exception");
            }


           
        }

    }

}

