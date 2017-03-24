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
            board.Status().ShouldBe(BoardStatus.InProgress);
        }

        public void should_put_correct_player_on_board_for_each_move()
        {
            var board = new Board(5);

            int x = 0, y = 4;
            board.AddMove(x, y);
            board.PlayerInCell(x, y).ShouldBe(Player.One);

            x = 4;
            y = 0;
            board.AddMove(x, y);
            board.PlayerInCell(x, y).ShouldBe(Player.Two);

            x = 4;
            y = 4;
            board.AddMove(x, y);
            board.PlayerInCell(x, y).ShouldBe(Player.One);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_1()
        {
            var board = new Board(4);

            board.AddMove(0, 0); // p1
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(1, 0);
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(1, 1); // p1
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(2, 0);
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(2, 2); // p1
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(3, 0);
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(3, 3); // p1 <----- just won
            board.Status().ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Player.One);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_2()
        {
            var board = new Board(4);

            board.AddMove(0, 0);
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(3, 0); // p2
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(1, 1);
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(3, 1); // p2
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(2, 2);
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(3, 2); // p2
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(2, 3);
            board.Status().ShouldBe(BoardStatus.InProgress);
            board.Winner.ShouldBe(Player.None);

            board.AddMove(3, 3); // p2 just won
            board.Status().ShouldBe(BoardStatus.Won);
            board.Winner.ShouldBe(Player.Two);
        }

        public void should_detect_when_game_is_tied()
        {
            var board = new Board(5);

            board.AddMove(0, 0); // p1
            board.AddMove(0, 1);

            board.AddMove(1, 1); // p1            
            board.AddMove(2, 2);

            board.AddMove(3, 3); // p1
            board.AddMove(4, 4);

            board.AddMove(0, 2); // p1
            board.AddMove(1, 2);

            board.AddMove(3, 2); // p1
            board.AddMove(3, 1);

            board.AddMove(4, 0); // p1
            board.AddMove(3, 0);

            board.AddMove(4, 3); // p1
            board.AddMove(2, 3);

            board.AddMove(2, 4); // p1
            // game is tied at this point; no result is possible even with additional moves

            board.Status().ShouldBe(BoardStatus.Drawn);
            board.Winner.ShouldBe(Player.None);
        }

        public void should_throw_when_adding_move_if_game_is_already_over()
        {
            var board = new Board(5);

            board.AddMove(0, 0); // p1
            board.AddMove(0, 1);

            board.AddMove(1, 1); // p1            
            board.AddMove(2, 2);

            board.AddMove(3, 3); // p1
            board.AddMove(4, 4);

            board.AddMove(0, 2); // p1
            board.AddMove(1, 2);

            board.AddMove(3, 2); // p1
            board.AddMove(3, 1);

            board.AddMove(4, 0); // p1
            board.AddMove(3, 0);

            board.AddMove(4, 3); // p1
            board.AddMove(2, 3);

            board.AddMove(2, 4); // p1
            // game is tied at this point; no result is possible even with additional moves

            Should.Throw<ApplicationException>(() =>
            {
                board.AddMove(2, 1);
            });
        }

    }
}
