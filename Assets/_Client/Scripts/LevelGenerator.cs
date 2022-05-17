using UnityEngine;
using System.Collections.Generic;

namespace Ball_Radar
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private Cell _cell;
        [SerializeField]
        private int _cellCount;

        [SerializeField]
        private Coin _coin;
        public Coin GetCoin
        {
            get => _coin;
            private set => _coin = value;
        }
        [SerializeField]
        private Obstacle _obstacle;
        public Obstacle GetObstacle
        {
            get => _obstacle;
            private set => _obstacle = value;
        }

        private List<Cell> myCells = new List<Cell>();

        private int _curCell;

        private void Start()
        {
            GenerateStartCells(_cellCount);
        }

        /// <summary>
        /// Generate Start Cells
        /// </summary>
        /// <param name="count">Count Cellls</param>
        private void GenerateStartCells(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Cell gCell = Instantiate(_cell, Vector3.up * _cell.CellHeight * _curCell, Quaternion.identity, transform);
                gCell.ActivateCell(this);
                myCells.Add(gCell);
                _curCell++;
            }
        }
    }
}
