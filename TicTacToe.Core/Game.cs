namespace TicTacToe.Core
{
    using System;

    public class Game
    {
        private Player _nextPlayer = Player.One;

        public Board Board { get; }
        public GameStatus Status { get; private set; } = GameStatus.InProgress;
        public Player Winner { get; private set; } = Player.None;

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

            Board.AddMoveToCell(x, y, _nextPlayer);

            UpdateGameStatus(_nextPlayer);

            _nextPlayer = _nextPlayer.Toggle();
        }

        private void UpdateGameStatus(Player player)
        {
            if (Board.IsWon())
            {
                Status = GameStatus.Completed;
                Winner = player;
            }
            else if (Board.IsDrawn())
            {
                Status = GameStatus.Completed;
                Winner = Player.None;
            }
        }
    }
}
