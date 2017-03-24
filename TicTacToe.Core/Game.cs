namespace TicTacToe.Core
{
    using System;

    public class Game
    {
        public Board Board { get; }
        public GameStatus Status { get; private set; } = GameStatus.InProgress;

        public Game()
        {
            Board = new Board();
        }

        public Game(int side)
        {
            Board = new Board(side);
        }

        public void AddMove(int x, int y)
        {
            if(Status == GameStatus.Completed)
                throw new ApplicationException("Game is already over!");

            Board.AddMoveToCell(x, y);

            UpdateGameStatus();
        }

        private void UpdateGameStatus()
        {
            if (Board.IsDrawn()|| Board.IsWon())
                Status = GameStatus.Completed;
        }
    }
}
