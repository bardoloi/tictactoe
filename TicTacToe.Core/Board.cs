﻿namespace TicTacToe.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Board
    {
        private const int DefaultSide = 3;
        private readonly List<Move> _moves = new List<Move>();
        private Player _nextPlayer = Player.One;

        public Player Winner { get; private set; } = Player.None;
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

        public BoardStatus Status()
        {
            if (IsWon())
                return BoardStatus.Won;

            if (IsDrawn())
                return BoardStatus.Drawn;

            return BoardStatus.InProgress;
        }

        public Player PlayerInCell(int x, int y)
        {
            var move = _moves.FirstOrDefault(m => m.X == x && m.Y == y);
            return move == null ? Player.None : move.Player;
        }

        public bool IsCellEmpty(int x, int y)
        {
            return !_moves.Any(m => m.X == x && m.Y == y);
        }

        public void AddMove(int x, int y)
        {
            if (x < 0 || x >= Side || y < 0 || y >= Side)
                throw new ArgumentException("Move falls outside board");
            if (!IsCellEmpty(x, y))
                throw new ArgumentException("Cell is already occupied");
            if (!Status().Equals(BoardStatus.InProgress))
                throw new ApplicationException("Game is already over!");

            var player = _nextPlayer;

            _moves.Add(new Move {Player = player, X = x, Y = y});

            if (IsWon())
                Winner = player;

            // toggle next player; move is marked
            _nextPlayer = player.Toggle(); 
        }

        internal bool IsWon()
        {
            return
                GetAllRows().Any(IsFilledBySinglePlayer)
                ||
                GetAllColumns().Any(IsFilledBySinglePlayer)
                ||
                GetAllDiagonals().Any(IsFilledBySinglePlayer);
        }

        internal bool IsDrawn()
        {
            // Drawn if each row, col and diag has at least 1 move by each players - so nobody can win
            return
                GetAllRows().All(ContainsMarksFromEachPlayer)
                &&
                GetAllColumns().All(ContainsMarksFromEachPlayer)
                &&
                GetAllDiagonals().All(ContainsMarksFromEachPlayer);
        }

        private static bool ContainsMarksFromEachPlayer(IEnumerable<Move> input)
        {
            IEnumerable<Move> x = input.ToList();
            return x.Any(m => m.Player == Player.One) && x.Any(m => m.Player == Player.Two);
        }

        private bool IsFilledBySinglePlayer(IEnumerable<Move> input)
        {
            IEnumerable<Move> x = input.ToList();
            return
                // row/col/diag is full
                x.Count() == Side &&
                // all cells are marked by the same player
                (x.All(cell => cell.Player == Player.One) || x.All(cell => cell.Player == Player.Two)); 
        }

        private IEnumerable<IEnumerable<Move>> GetAllRows()
        {
            var rows = new List<IEnumerable<Move>>();
            for(var i = 0; i < Side; i++)
                rows.Add(GetRow(i));

            return rows.AsEnumerable();
        }

        private IEnumerable<IEnumerable<Move>> GetAllColumns()
        {
            var cols = new List<IEnumerable<Move>>();
            for (var i = 0; i < Side; i++)
                cols.Add(GetColumn(i));

            return cols.AsEnumerable();
        }

        private IEnumerable<IEnumerable<Move>> GetAllDiagonals()
        {
            var diags = new List<IEnumerable<Move>> {GetDiagonal(), GetAntiDiagonal()};
            return diags.AsEnumerable();
        }

        private IEnumerable<Move> GetRow(int rowIndex)
        {
            return _moves.Where(m => m.X == rowIndex);
        }

        private IEnumerable<Move> GetColumn(int colIndex)
        {
            return _moves.Where(m => m.Y == colIndex);
        }

        private IEnumerable<Move> GetDiagonal()
        {
            return _moves.Where(m => m.Y == m.X);
        }

        private IEnumerable<Move> GetAntiDiagonal()
        {
            return _moves.Where(m => m.Y + m.X + 1 == Side);
        }
    }
}
