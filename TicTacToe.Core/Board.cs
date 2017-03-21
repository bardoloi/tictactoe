namespace TicTacToe.Core
{
    using System;

    public class Board
    {
        public const int SIZE = 9, SIDE = 3;

        private Player[][] _cells;
        private int _filledCells;

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
            return (
                // row win
                (_cells[0][0] == _cells[0][1] && _cells[0][0] == _cells[0][2] && _cells[0][0] != Player.None) ||
                (_cells[1][0] == _cells[1][1] && _cells[1][0] == _cells[1][2] && _cells[1][0] != Player.None) ||
                (_cells[2][0] == _cells[2][1] && _cells[2][0] == _cells[2][2] && _cells[2][0] != Player.None) ||
                // column win
                (_cells[0][0] == _cells[1][0] && _cells[0][0] == _cells[2][0] && _cells[0][0] != Player.None) ||
                (_cells[1][0] == _cells[1][1] && _cells[1][0] == _cells[1][2] && _cells[1][0] != Player.None) ||
                (_cells[2][0] == _cells[2][1] && _cells[2][0] == _cells[2][2] && _cells[2][0] != Player.None) ||
                // diagonal win
                (_cells[0][0] == _cells[1][1] && _cells[0][0] == _cells[2][2] && _cells[0][0] != Player.None) ||
                (_cells[2][0] == _cells[1][1] && _cells[2][0] == _cells[0][2] && _cells[2][0] != Player.None)
            );
        }

        public bool IsFull()
        {
            return (_filledCells == 9);
        }
    }
}
