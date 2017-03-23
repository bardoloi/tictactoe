namespace TicTacToe.Core
{
    using System;
    using System.Linq;

    public class Board
    {
        public const int SIZE = 9, SIDE = 3;

        private Player[][] _cells;
        private int _filledCells;
        private int[] _rowColDiagScores = new int[(2 * SIDE) + 2];

        public Board()
        {
            // initialize a 3x3 board
            _cells = new Player[SIDE][];
            for (var i = 0; i < SIDE; i++)
            {
                _cells[i] = new [] { Player.None, Player.None, Player.None };
            }
        }

        internal void AddMoveToCell(int x, int y, Player player)
        {
            if (x < 0 || x >= SIDE || y < 0 || y >= SIDE)
                throw new ArgumentException("Move falls outside board");
            if (!IsCellEmpty(x, y))
                throw new ArgumentException("Cell is already occupied");

            _cells[x][y] = player;

            _filledCells++;

            UpdateInternalScore(x, y, player);
        }

        private void UpdateInternalScore(int x, int y, Player player)
        {
            var score = (player == Player.Player1) ? 1 : -1;

            var rowIndex = x;
            var colIndex = SIDE + y;

            const int diag1Index = 2*SIDE;
            const int diag2Index = 2 * SIDE + 1;

            _rowColDiagScores[rowIndex] += score; // update row score
            _rowColDiagScores[colIndex] += score; // update column score

            if (x == y) 
                _rowColDiagScores[diag1Index] += score; // update diagonal score
            else if (x + y == SIDE) 
                _rowColDiagScores[diag2Index] += score; // update antidiagonal score
        }

        public Player PlayerInCell(int x, int y)
        {
            return _cells[x][y];
        }

        public bool IsCellEmpty(int x, int y)
        {
            return _cells[x][y] == Player.None;
        }

        public bool IsWon()
        {
            return _rowColDiagScores.Any(s => Math.Abs(s).Equals(SIDE));
        }

        public bool IsFull()
        {
            return (_filledCells == 9);
        }
    }
}
