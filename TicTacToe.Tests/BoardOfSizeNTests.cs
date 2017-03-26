namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class BoardOfSizeNTests
    {
        public void should_throw_if_creating_board_of_side_less_than_3()
        {
            for(var i = 0; i < 3; i++)
                Should.Throw<ArgumentException>(() =>
                {
                    var board = new Board(i);
                });
        }

        public void should_create_new_board_with_status_in_progress()
        {
            var board = new Board(6);
            board.Status.ShouldBe(BoardStatus.InProgress);
        }

        public void should_put_correct_player_on_board_for_each_move()
        {
            var board = new Board(5);

            int x = 0, y = 4;
            board.AddMark(x, y, Mark.X);
            board.MarkInCell(x, y).ShouldBe(Mark.X);

            x = 4;
            y = 0;
            board.AddMark(x, y, Mark.O);
            board.MarkInCell(x, y).ShouldBe(Mark.O);

            x = 4;
            y = 4;
            board.AddMark(x, y, Mark.X);
            board.MarkInCell(x, y).ShouldBe(Mark.X);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_1()
        {
            var board = new Board(4);

            board.AddMark(0, 0, Mark.X); // p1
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 0, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 1, Mark.X); // p1
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(2, 0, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(2, 2, Mark.X); // p1
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(3, 0, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(3, 3, Mark.X); // p1 <----- just won
            board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Mark.X);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_2()
        {
            var board = new Board(4);

            board.AddMark(0, 0, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(3, 0, Mark.X); // p2
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(1, 1, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(3, 1, Mark.X); // p2
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(2, 2, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(3, 2, Mark.X); // p2
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(2, 3, Mark.O);
            board.Status.ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Mark.None);

            board.AddMark(3, 3, Mark.X); // p2 just won
            board.Status.ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Mark.X);
        }

        public void should_detect_when_game_is_tied()
        {
            var board = new Board(5);

            board.AddMark(0, 0, Mark.X); // p1
            board.AddMark(0, 1, Mark.O);

            board.AddMark(1, 1, Mark.X); // p1            
            board.AddMark(2, 2, Mark.O);

            board.AddMark(3, 3, Mark.X); // p1
            board.AddMark(4, 4, Mark.O);

            board.AddMark(0, 2, Mark.X); // p1
            board.AddMark(1, 2, Mark.O);

            board.AddMark(3, 2, Mark.X); // p1
            board.AddMark(3, 1, Mark.O);

            board.AddMark(4, 0, Mark.X); // p1
            board.AddMark(3, 0, Mark.O);

            board.AddMark(4, 3, Mark.X); // p1
            board.AddMark(2, 3, Mark.O);

            board.AddMark(2, 4, Mark.X); // p1
            // game is tied at this point; no result is possible even with additional moves

            board.Status.ShouldBe(BoardStatus.Drawn);
            board.Winner.ShouldBe(Mark.None);
        }

        public void should_throw_when_adding_move_if_game_is_already_over()
        {
            var board = new Board(5);

            board.AddMark(0, 0, Mark.O); // p1
            board.AddMark(0, 1, Mark.X);

            board.AddMark(1, 1, Mark.O); // p1            
            board.AddMark(2, 2, Mark.X);

            board.AddMark(3, 3, Mark.O); // p1
            board.AddMark(4, 4, Mark.X);

            board.AddMark(0, 2, Mark.O); // p1
            board.AddMark(1, 2, Mark.X);

            board.AddMark(3, 2, Mark.O); // p1
            board.AddMark(3, 1, Mark.X);

            board.AddMark(4, 0, Mark.O); // p1
            board.AddMark(3, 0, Mark.X);

            board.AddMark(4, 3, Mark.O); // p1
            board.AddMark(2, 3, Mark.X);

            board.AddMark(2, 4, Mark.O); // p1
            // game is tied at this point; no result is possible even with additional moves

            Should.Throw<ApplicationException>(() =>
            {
                board.AddMark(2, 1, Mark.X);
            });
        }

    }
}
