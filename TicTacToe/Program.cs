namespace TicTacToe
{
    using System;
    using System.Linq;
    using Core;

    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();

            var mark = Mark.X; // always start with X
            while (board.Status.Equals(BoardStatus.InProgress))
            {
                try
                {
                    Console.Write("Enter next move (x y): ");
                    var xy = Array.ConvertAll(Console.ReadLine().Split(' ').ToArray(), s => int.Parse(s.Trim()));

                    board.AddMark(xy[0], xy[1], mark);
                    mark = mark.Toggle();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                DisplayBoard(board);
            }

            Console.WriteLine($"{board.Winner.DisplayName} wins!");

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
                    if (board.MarkInCell(i, j) == Mark.X)
                    {
                        mark = "X";
                    }
                    else if (board.MarkInCell(i, j) == Mark.O)
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
