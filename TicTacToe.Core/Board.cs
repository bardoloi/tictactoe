namespace TicTacToe.Core
{
    using System;
    using System.Linq;

    public class Board
    {
        private const int MinimumSide = 3;

        private int _filledCells;
        private readonly Player[][] _cells;
        private readonly int[] _rowColDiagScores, _rowColDiagMoveCounts;
        public int Side { get; }

        public Board() : this(MinimumSide)
        {            
        }

        public Board(int side)
        {
            if (side < Board.MinimumSide)
                throw new ArgumentException($"Board size must be {MinimumSide} or higher");

            Side = side;
            _rowColDiagScores = new int[(2 * Side) + 2];
            _rowColDiagMoveCounts = new int[(2 * Side) + 2];

            // initialize board
            _cells = new Player[Side][];
            for (var i = 0; i < Side; i++)
            {
                _cells[i] = new Player[Side];
                for (var j = 0; j < Side; j++)
                    _cells[i][j] = Player.None;
            }
        }

        internal void AddMoveToCell(int x, int y, Player player)
        {
            if (x < 0 || x >= Side || y < 0 || y >= Side)
                throw new ArgumentException("Move falls outside board");
            if (!IsCellEmpty(x, y))
                throw new ArgumentException("Cell is already occupied");

            _cells[x][y] = player;

            _filledCells++;

            UpdateInternalCounts(x, y, player);
        }

        private void UpdateInternalCounts(int x, int y, Player player)
        {
            var score = (player == Player.One) ? 1 : -1;

            var rowIndex = x;
            var colIndex = Side + y;

            var diag1Index = 2* Side;
            var diag2Index = 2 * Side + 1;

            // update row scores
            _rowColDiagScores[rowIndex] += score; 
            _rowColDiagMoveCounts[rowIndex]++;

            // update column score
            _rowColDiagScores[colIndex] += score; 
            _rowColDiagMoveCounts[colIndex]++;

            // update diagonal score
            if (x == y)
            {
                _rowColDiagScores[diag1Index] += score; 
                _rowColDiagMoveCounts[diag1Index]++;
            }
            
            // update antidiagonal score
            if (x + y + 1 == Side)
            { 
                _rowColDiagScores[diag2Index] += score;
                _rowColDiagMoveCounts[diag2Index]++;
            }
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
            return _rowColDiagScores.Any(s => Math.Abs(s).Equals(Side));
        }

        public bool IsDrawn()
        {
            for (var i = 0; i < _rowColDiagScores.Length; i++)
                if (_rowColDiagMoveCounts[i] == Math.Abs(_rowColDiagScores[i]))
                    return false;

            return true;
        }
    }
}
