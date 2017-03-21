using System;

namespace TicTacToe.Core
{
    public class Game
    {
        public const int PLAYER1 = 1, PLAYER2 = -1, NONE = 0;
        public const string COMPLETE = "COMPLETE", INPROGRESS = "IN PROGRESS";

        private int _nextPlayer = PLAYER1;

        public Board Board { get; }
        public string Status { get; private set; }
        public int Winner { get; private set; }

        public Game()
        {
            Board = new Board();
            Status = INPROGRESS;
            Winner = NONE;
        }

        public void AddMove(int x, int y)
        {
            if(Status == COMPLETE)
                throw new ApplicationException("Game is already over!");

            Board.AddMoveToCell(x, y, _nextPlayer);

            UpdateGameStatus(_nextPlayer);

            _nextPlayer = (_nextPlayer == PLAYER1) ? PLAYER2 : PLAYER1;
        }

        private void UpdateGameStatus(int player)
        {
            if (Board.IsWon())
            {
                Status = COMPLETE;
                Winner = player;
            }
            else if (Board.IsFull())
            {
                Status = COMPLETE;
                Winner = NONE;
            }
        }
    }
}
