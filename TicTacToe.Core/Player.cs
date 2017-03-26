namespace TicTacToe.Core
{
    public class Player
    {
        public Player(Mark mark, string name)
        {
            Mark = mark;
            Name = name;
        }

        public Mark Mark { get; }
        public string Name { get; }
    }
}