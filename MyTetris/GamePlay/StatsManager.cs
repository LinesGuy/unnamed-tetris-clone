using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris.GamePlay
{
    public class StatsManager
    {
        private TetrisGame _game;
        public StatsManager(TetrisGame game)
        {
            _game = game;
        }
        private void WriteLine(SpriteBatch spriteBatch, string text, int rowNumber)
        {
            spriteBatch.DrawString(Assets.NovaSquare24, text, _game.Position + new Vector2(_game.PlayField.Width * TetrisGame.TILE_SIZE + 32, 128 + 32 * rowNumber), Color.White);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            WriteLine(spriteBatch, $"Subtiles: {_game.CurrentPiece.SubTiles}", 0);
            WriteLine(spriteBatch, $"LineClearFramesToWait: {_game.PlayField.LineClearFramesToWait}", 1);
            WriteLine(spriteBatch, $"GroundedFrames: {_game.CurrentPiece.GroundedFrames}", 2);
            WriteLine(spriteBatch, $"CurrentPiece FramesToWait: {_game.CurrentPiece.AppearanceDelay}", 3);
            WriteLine(spriteBatch, $"Piece id/orientation: {_game.CurrentPiece.Id}, {_game.CurrentPiece.Orientation}", 4);
        }
    }
}