namespace TicTacToe.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Board
    {
        private const int DefaultSide = 3;
        private readonly List<Cell> _cells = new List<Cell>();
        private Mark _lastMark = Mark.None;

        public Mark Winner { get; private set; } = Mark.None;
        public int Side { get; }

        public Board() : this(DefaultSide)
        {            
        }

        public Board(int side)
        {
            if (side < DefaultSide)
                throw new ArgumentException($"Board size must be {DefaultSide} or higher");

            Side = side;
        }

        public BoardStatus Status
        {
            get
            {
                if (IsWon())
                    return BoardStatus.Won;

                if (IsDrawn())
                    return BoardStatus.Drawn;

                return BoardStatus.InProgress;
            }
        }

        public Mark MarkInCell(int x, int y)
        {
            var cell = _cells.FirstOrDefault(m => m.X == x && m.Y == y);
            return cell == null ? Mark.None : cell.Mark;
        }

        public bool IsCellEmpty(int x, int y)
        {
            return !_cells.Any(m => m.X == x && m.Y == y);
        }

        public void AddMove(int x, int y, Mark mark)
        {
            if (!Status.Equals(BoardStatus.InProgress))
                throw new ApplicationException("Game is already over!");
            if (x < 0 || x >= Side || y < 0 || y >= Side)
                throw new ArgumentException("Move falls outside board");
            if (!IsCellEmpty(x, y))
                throw new ArgumentException("Cell is already occupied");
            if(mark == _lastMark)
                throw new ArgumentException("X and O must alternate, they cannot repeat!");
            if (mark == Mark.None)
                throw new ArgumentException("Must play either X or O");

            _cells.Add(new Cell {Mark = mark, X = x, Y = y});

            if (IsWon())
                Winner = mark;

            _lastMark = mark; 
        }

        private bool IsWon()
        {
            return
                GetAllRows().Any(IsFilledWithOneMark)
                ||
                GetAllColumns().Any(IsFilledWithOneMark)
                ||
                GetAllDiagonals().Any(IsFilledWithOneMark);
        }

        private bool IsDrawn()
        {
            // Drawn if each row, col and diag has at least 1 move by each players - so nobody can win
            return
                GetAllRows().All(ContainsEachMarkAtLeastOnce)
                &&
                GetAllColumns().All(ContainsEachMarkAtLeastOnce)
                &&
                GetAllDiagonals().All(ContainsEachMarkAtLeastOnce);
        }

        private static bool ContainsEachMarkAtLeastOnce(IEnumerable<Cell> input)
        {
            IEnumerable<Cell> x = input.ToList();
            return x.Any(m => m.Mark == Mark.X) && x.Any(m => m.Mark == Mark.O);
        }

        private bool IsFilledWithOneMark(IEnumerable<Cell> input)
        {
            IEnumerable<Cell> x = input.ToList();
            return
                // row/col/diag is full
                x.Count() == Side &&
                // all cells are marked by the same player
                (x.All(cell => cell.Mark == Mark.X) || x.All(cell => cell.Mark == Mark.O)); 
        }

        private IEnumerable<IEnumerable<Cell>> GetAllRows()
        {
            var rows = new List<IEnumerable<Cell>>();
            for(var i = 0; i < Side; i++)
                rows.Add(GetRow(i));

            return rows.AsEnumerable();
        }

        private IEnumerable<IEnumerable<Cell>> GetAllColumns()
        {
            var cols = new List<IEnumerable<Cell>>();
            for (var i = 0; i < Side; i++)
                cols.Add(GetColumn(i));

            return cols.AsEnumerable();
        }

        private IEnumerable<IEnumerable<Cell>> GetAllDiagonals()
        {
            var diags = new List<IEnumerable<Cell>> {GetDiagonal(), GetAntiDiagonal()};
            return diags.AsEnumerable();
        }

        private IEnumerable<Cell> GetRow(int rowIndex)
        {
            return _cells.Where(m => m.X == rowIndex);
        }

        private IEnumerable<Cell> GetColumn(int colIndex)
        {
            return _cells.Where(m => m.Y == colIndex);
        }

        private IEnumerable<Cell> GetDiagonal()
        {
            return _cells.Where(m => m.Y == m.X);
        }

        private IEnumerable<Cell> GetAntiDiagonal()
        {
            return _cells.Where(m => m.Y + m.X + 1 == Side);
        }
    }
}
