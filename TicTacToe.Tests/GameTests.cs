namespace TicTacToe.Tests
{
    using Core;
    using Shouldly;

    public class GameTests
    {
        public void should_add_moves_in_correct_player_order()
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

        public void should_detect_when_game_is_won_by_player_1()
        {
            var game = new Game();

            game.AddMove(0, 0); // p1
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(1, 0);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(0, 1); // p1
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(1, 1);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(0, 2); // p1 just won
            game.Status.ShouldBe(Game.COMPLETE);
            game.Winner.ShouldBe(Game.PLAYER1);            
        }

        public void should_detect_when_game_is_won_by_player_2()
        {
            var game = new Game();

            game.AddMove(0, 0);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(1, 0); // p2
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(0, 1);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(1, 1); // p2
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(2, 2);
            game.Status.ShouldBe(Game.INPROGRESS);
            game.AddMove(1, 2); // p2 just won
            game.Status.ShouldBe(Game.COMPLETE);
            game.Winner.ShouldBe(Game.PLAYER2);
        }

        public void should_detect_when_game_is_tied()
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
            game.Status.ShouldBe(Game.COMPLETE);
            game.Winner.ShouldBe(Game.NONE);
        }
    }
}