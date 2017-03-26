namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class GameTests
    {
        public void should_begin_default_game_with_standard_board_size()
        {
            var game = new Game(Mark.X);
            game.Board.Side.ShouldBe(3);
        }

        public void should_begin_default_game_with_status_in_progress()
        {
            var game = new Game(Mark.X);
            game.Status.ShouldBe(GameStatus.InProgress);
        }

        public void should_begin_custom_game_with_custom_board_size()
        {
            var game = new Game(Mark.X, 5);
            game.Board.Side.ShouldBe(5);
        }

        public void should_throw_if_constructor_receives_mark_none()
        {
            Should.Throw<ArgumentException>(() =>
            {
                var game = new Game(Mark.None);
            });
            Should.Throw<ArgumentException>(() =>
            {
                var game = new Game(Mark.None, 5);
            });
        }

        public void should_mark_board_correctly_based_on_player_1s_mark()
        {
            var game = new Game(Mark.X);

            int x = 0, y = 1;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.X);

            x = 1;
            y = 1;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.O);

            x = 1;
            y = 2;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.X);

            x = 2;
            y = 1;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.O);
        }

        public void should_toggle_player_marks_correctly_after_each_move()
        {
            var game = new Game(Mark.X);

            int x = 0, y = 1;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.X);

            x = 1;
            y = 1;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.O);

            x = 1;
            y = 2;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.X);

            x = 2;
            y = 1;
            game.AddMove(x, y);
            game.Board.MarkInCell(x, y).ShouldBe(Mark.O);
        }

        public void should_detect_status_and_winner_when_player_1_wins()
        {
            var game = new Game(Mark.X);

            game.AddMove(0, 0); // p1
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(1, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(0, 1); // p1
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(1, 1);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(0, 2); // p1 <--- just won
            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(game.Player1);
        }

        public void should_detect_status_and_winner_when_player_2_wins()
        {
            var game = new Game(Mark.X);

            game.AddMove(0, 0);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(1, 0); // p2
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(0, 1);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(1, 1); // p2
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(2, 2);
            game.Status.ShouldBe(GameStatus.InProgress);
            game.Winner.ShouldBe(null);

            game.AddMove(1, 2); // p2 <----- just won
            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(game.Player2);
        }

        public void should_update_status_and_winner_when_game_is_drawn()
        {
            var game = new Game(Mark.O);

            game.AddMove(0, 0); // p1
            game.AddMove(0, 1);
            game.AddMove(0, 2); // p1            
            game.AddMove(1, 1);
            game.AddMove(2, 1); // p1
            game.AddMove(1, 0);
            game.AddMove(1, 2); // p1
            game.AddMove(2, 2); // tie game

            game.Status.ShouldBe(GameStatus.Completed);
            game.Winner.ShouldBe(null);
        }
    }
}
