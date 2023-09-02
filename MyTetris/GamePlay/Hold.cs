using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris.GamePlay
{
    public class Hold
    {
        private TetrisGame _game;
        public int HeldPiece = -1;
        public bool JustHeld = false;
        public Hold(TetrisGame game)
        {
            _game = game;
        }
        /// <summary>
        /// Swaps the current piece with the piece in hold, or if there is none, places the current piece in hold and gives the player the next piece. The player cannot swap the same piece twice in a row. />
        /// </summary>
        /// <returns>Whether or not the current piece was swapped. Swapping with the blank piece counts as a success.</returns>
        public bool SwapHold()
        {
            if (JustHeld)
            {
                return false;
            }
            JustHeld = true;
            if (HeldPiece == -1)
            {
                HeldPiece = _game.CurrentPiece.Id;
                _game.CurrentPiece.Id = _game.NextPieces.Next();
            }
            else
            {
                int toHold = _game.CurrentPiece.Id;
                _game.CurrentPiece.Id = HeldPiece;
                HeldPiece = toHold;
            }
            return true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (HeldPiece == -1) return;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (PieceData.Tiles[HeldPiece, 0][x + y * 4] == '0')
                        continue;
                    spriteBatch.Draw(Assets.BlockW[HeldPiece], Utils.RectangleF(_game.Position + new Vector2(x - 1, y - 1) * TetrisGame.TILE_SIZE, new Vector2(TetrisGame.TILE_SIZE)), Color.White);
                }
            }
        }
    }
}
