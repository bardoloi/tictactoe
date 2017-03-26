namespace TicTacToe.Core
{
    using System;

    public class Game
    {
        private Mark _nextPlayerMark;

        public Game(Mark firstMark) : this(firstMark, 3)
        {
        }

        public Game(Mark firstMark, int side)
        {
            if(firstMark.Equals(Mark.None))
                throw new ArgumentException("First mark must be X or O.");

            Board = new Board(side);
            Player1 = new Player(firstMark, "Player 1");
            Player2 = new Player(firstMark.Toggle(), "Player 2");

            _nextPlayerMark = firstMark;
        }

        public Board Board { get; }

        public Player Player1 { get; }
        public Player Player2 { get; }

        public Player Winner
        {
            get
            {
                if (Board.Winner.Equals(Player1.Mark))
                    return Player1;

                if (Board.Winner.Equals(Player2.Mark))
                    return Player2;

                return null;                
            }
        }

        public GameStatus Status => Board.Status.Equals(BoardStatus.InProgress) 
            ? GameStatus.InProgress 
            : GameStatus.Completed;

        public void AddMove(int x, int y)
        {
            Board.AddMove(x, y, _nextPlayerMark);
            _nextPlayerMark = _nextPlayerMark.Toggle();
        }
    }
}
