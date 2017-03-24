namespace TicTacToe.Core
{
    using System;

    public class Game
    {
        public const string COMPLETE = "COMPLETE", INPROGRESS = "IN PROGRESS";

        private Player _nextPlayer = Player.One;

        public Board Board { get; }
        public string Status { get; private set; } = INPROGRESS;
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
            if(Status == COMPLETE)
                throw new ApplicationException("Game is already over!");

            Board.AddMoveToCell(x, y, _nextPlayer);

            UpdateGameStatus(_nextPlayer);

            _nextPlayer = _nextPlayer.Toggle();
        }

        private void UpdateGameStatus(Player player)
        {
            if (Board.IsWon())
            {
                Status = COMPLETE;
                Winner = player;
            }
            else if (Board.IsComplete())
            {
                Status = COMPLETE;
                Winner = Player.None;
            }
        }
    }
}
