namespace TicTacToe.Core
{
    using System;

    public class Game
    {
        public const string COMPLETE = "COMPLETE", INPROGRESS = "IN PROGRESS";

        private Player _nextPlayer = Player.Player1;

        public Board Board { get; }
        public string Status { get; private set; } = INPROGRESS;
        public Player Winner { get; private set; } = Player.None;

        public Game()
        {
            Board = new Board();
        }

        public Game(int side)
        {
            if (side < Board.DEFAULTSIDE)
                throw new ArgumentException("Board size must be 3 or higher");
            Board = new Board(side);
        }

        public void AddMove(int x, int y)
        {
            if(Status == COMPLETE)
                throw new ApplicationException("Game is already over!");

            Board.AddMoveToCell(x, y, _nextPlayer);

            UpdateGameStatus(_nextPlayer);

            _nextPlayer = (_nextPlayer == Player.Player1) ? Player.Player2 : Player.Player1;
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
