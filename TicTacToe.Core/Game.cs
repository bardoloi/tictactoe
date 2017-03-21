namespace TicTacToe.Core
{
    using System;

    public class Game
    {
        public const string COMPLETE = "COMPLETE", INPROGRESS = "IN PROGRESS";

        private Player _nextPlayer;

        public Board Board { get; }
        public string Status { get; private set; }
        public Player Winner { get; private set; }

        public Game()
        {
            Board = new Board();
            Status = INPROGRESS;
            Winner = Player.None;
            _nextPlayer = Player.Player1;
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
            else if (Board.IsFull())
            {
                Status = COMPLETE;
                Winner = Player.None;
            }
        }
    }
}
