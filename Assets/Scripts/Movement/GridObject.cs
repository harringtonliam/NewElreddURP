using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement
{
    public class GridObject
    {
        private GridSystem gridSystem;
        private GridPosition gridPostion;
        private List<Mover> moverList;

        public  GridObject(GridSystem gridSystem, GridPosition gridPostion)
        {
            this.gridPostion = gridPostion;
            this.gridSystem = gridSystem;
            moverList = new List<Mover>();
        }

        public override string ToString()
        {
            if (moverList == null)
            {
                return $"{gridPostion.ToString()}";
            }
            else
            {
                string moverString = "";
                foreach (var mover in moverList)
                {
                    moverString += "\n" + mover;
                }
                return $"{gridPostion.ToString()}, \n Unit:{moverString}";
            }
            
        }

        public List<Mover> GetMoverList()
        {
            return moverList;
        }

        public void AddMover(Mover mover)
        {
            moverList.Add(mover);
        }

        public void RemoveMover(Mover mover)
        {
            moverList.Remove(mover);
        }

        public bool HasAnyUnit()
        {
            return moverList.Count > 0;
        }

    }

}


