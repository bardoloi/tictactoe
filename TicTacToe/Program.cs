namespace TicTacToe
{
    using System;
    using System.Linq;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            while (game.Status != GameStatus.Completed)
            {
                try
                {
                    Console.Write("Enter next move (x y): ");
                    var xy = Array.ConvertAll(Console.ReadLine().Split(' ').ToArray(), s => int.Parse(s.Trim()));

                    game.AddMove(xy[0], xy[1]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                DisplayBoard(game.Board);
            }

            Console.WriteLine($"{game.Board.Winner.DisplayName} wins!");

            Console.ReadLine();
        }

        private static void DisplayBoard(Board board)
        {
            var output = "p0|p1|p2\n_ _ _\np3|p4|p5\n_ _ _\np6|p7|p8\n";
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var index = 3*i + j;

                    var mark = " ";
                    if (board.PlayerInCell(i, j) == Player.One)
                    {
                        mark = "X";
                    }
                    else if (board.PlayerInCell(i, j) == Player.Two)
                    {
                        mark = "O";
                    }

                    output = output.Replace($"p{index}", mark);
                }
            }
                    
            Console.WriteLine(output);
        }
    }
}
