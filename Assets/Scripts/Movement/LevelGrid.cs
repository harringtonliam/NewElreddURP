using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
    public class LevelGrid : MonoBehaviour
    {
        [SerializeField] Transform gridDebugObjectPrefab;
        [SerializeField] int gridXSize = 50;
        [SerializeField] int gridZSize = 50;

        public static LevelGrid Instance { get; private set; }

        private GridSystem gridSystem;

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than one level grid " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;

            gridSystem = new GridSystem(gridXSize, gridZSize, 2f);
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }


        public void AddUnitAtGridPosition(GridPosition gridPosition, Mover mover)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.AddMover(mover);
        }

        public List<Mover> GetUnitListAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetMoverList();
        }

        public void RemoveUnitAtGridPosition(Mover mover, GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveMover(mover);
        }

        public void UnitMovedGridPoistion(Mover mover, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            RemoveUnitAtGridPosition(mover, fromGridPosition);
            AddUnitAtGridPosition(toGridPosition, mover);
        }

        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

        public Vector3 GetGridWorld(GridPosition gridPostion) => gridSystem.GetWorldPosition(gridPostion);


        public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

        public int GetWith() => gridSystem.GetWidth();

        public int GetLength() => gridSystem.GetLength();

        public bool HasAnyUnitOnThisGridPosition( GridPosition gridPosition)
        {
            return gridSystem.GetGridObject(gridPosition).HasAnyUnit();
        }

    }

}


