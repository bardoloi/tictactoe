namespace TicTacToe.Tests
{
    using System;
    using Core;
    using Shouldly;

    public class GameTests
    {
        public void should_put_correct_player_on_board_for_each_move()
        {
            var game = new Game();

            int x = 0, y = 2;
            game.AddMove(x, y);
            game.Board.Cell(x, y).ShouldBe(Game.PLAYER1);

            x = 1;
            y = 0;
            game.AddMove(x, y);
            game.Board.Cell(x, y).ShouldBe(Game.PLAYER2);

            x = 1;
            y = 1;
            game.AddMove(x, y);
            game.Board.Cell(x, y).ShouldBe(Game.PLAYER1);
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_1()
        {
            var game = new Game();

            game.AddMove(0, 0); // p1
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(1, 0);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(0, 1); // p1
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(1, 1);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(0, 2); // p1 just won
            game.Status.ShouldBe(Game.COMPLETE);
            game.Winner.ShouldBe(Game.PLAYER1);            
        }

        public void should_update_status_and_winner_when_game_is_won_by_player_2()
        {
            var game = new Game();

            game.AddMove(0, 0);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(1, 0); // p2
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(0, 1);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(1, 1); // p2
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(2, 2);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.Winner.ShouldBe(Game.NONE);
            game.AddMove(1, 2); // p2 just won
            game.Status.ShouldBe(Game.COMPLETE);
            game.Winner.ShouldBe(Game.PLAYER2);
        }

        public void should_detect_when_game_is_tied()
        {
            var game = PlayTiedGame();
            game.Status.ShouldBe(Game.COMPLETE);
            game.Winner.ShouldBe(Game.NONE);
        }

        public void should_throw_when_adding_move_if_game_is_already_over()
        {
            var game = PlayTiedGame();
            Should.Throw<ApplicationException>(() =>
            {
                game.AddMove(2, 1);
            });
        }

        public void should_start_new_game_with_status_in_progress()
        {
            var game = new Game();
            game.Status.ShouldBe(Game.INPROGRESS);
        }

        private static Game PlayTiedGame()
        {
            var game = new Game();

            game.AddMove(0, 0); // p1
            game.AddMove(0, 1);
            game.AddMove(0, 2); // p1            
            game.AddMove(1, 1);
            game.AddMove(2, 1); // p1
            game.AddMove(1, 0);
            game.AddMove(1, 2); // p1
            game.AddMove(2, 2);
            game.AddMove(2, 0); // p1; tie

            return game;
        }
    }
}