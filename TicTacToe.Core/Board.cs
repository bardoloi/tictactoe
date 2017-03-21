namespace TicTacToe.Core
{
    using System;

    public class Board
    {
        public const int SIZE = 9, SIDE = 3;
        private const int EMPTYCELLINDICATOR = 0;
        private int[][] _cells;
        private int _filledCells;

        public Board()
        {
            // initialize a 3x3 board
            _cells = new int[3][];
            for (var i = 0; i < 3; i++)
            {
                _cells[i] = new [] { EMPTYCELLINDICATOR, EMPTYCELLINDICATOR, EMPTYCELLINDICATOR };
            }
        }

        public void AddMoveToCell(int x, int y, int playerNumber)
        {
            if (x < 0 || x >= SIDE || y < 0 || y >= SIDE)
                throw new ArgumentException("Move falls outside board");
            if (!IsCellEmpty(x, y))
                throw new ArgumentException("Cell is already occupied");

            _cells[x][y] = playerNumber;

            _filledCells++;
        }

        public int Cell(int x, int y)
        {
            return _cells[x][y];
        }

        public bool IsCellEmpty(int x, int y)
        {
            return _cells[x][y] == EMPTYCELLINDICATOR;
        }

        public bool IsWon()
        {
            return (
                // row win
                (_cells[0][0] == _cells[0][1] && _cells[0][0] == _cells[0][2] && _cells[0][0] != EMPTYCELLINDICATOR) ||
                (_cells[1][0] == _cells[1][1] && _cells[1][0] == _cells[1][2] && _cells[1][0] != EMPTYCELLINDICATOR) ||
                (_cells[2][0] == _cells[2][1] && _cells[2][0] == _cells[2][2] && _cells[2][0] != EMPTYCELLINDICATOR) ||
                // column win
                (_cells[0][0] == _cells[1][0] && _cells[0][0] == _cells[2][0] && _cells[0][0] != EMPTYCELLINDICATOR) ||
                (_cells[1][0] == _cells[1][1] && _cells[1][0] == _cells[1][2] && _cells[1][0] != EMPTYCELLINDICATOR) ||
                (_cells[2][0] == _cells[2][1] && _cells[2][0] == _cells[2][2] && _cells[2][0] != EMPTYCELLINDICATOR) ||
                // diagonal win
                (_cells[0][0] == _cells[1][1] && _cells[0][0] == _cells[2][2] && _cells[0][0] != EMPTYCELLINDICATOR) ||
                (_cells[2][0] == _cells[1][1] && _cells[2][0] == _cells[0][2] && _cells[2][0] != EMPTYCELLINDICATOR)
            );
        }

        public bool IsFull()
        {
            return (_filledCells == 9);
        }
    }
}
