using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.CodeAnalysis;

namespace MyTetris.GamePlay
{
    public class PlayField
    {
        private TetrisGame _game;
        public int[,] Tiles;
        public int Width = 10;
        public int Height = 24; // 24 rows minus 4 invisible rows means only the bottom 20 rows will be visible to the player, as is somewhat standard in Tetris
        private int _invisibleRows = 4;
        public Vector2 Offset; // Offset of the entire playfield
        public int LineClearFramesToWait = 0;
        public PlayField(TetrisGame game)
        {
            _game = game;
            Offset = game.Position + new Vector2(0, -60);
            Tiles = new int[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Tiles[x, y] = -1;
                }
            }
        }

        /// <summary>
        /// <para>Clears any filled in lines, and increments Level and SpeedLevel accordingly.</para>
        /// <para>Note that this does NOT immediately shift any floating blocks down, this is done after LineClear frames.</para>
        /// </summary>
        /// <returns> Whether or not any lines were cleared at all </returns>
        public bool ClearLines()
        {
            int linesCleared = 0;
            for (int y = 0; y < Height; y++)
            {
                int filledTiles = 0; // Number of non-empty tiles for this row
                for (int x = 0; x < Width; x++)
                {
                    if (Tiles[x, y] != -1) filledTiles++;
                }
                // If all the tiles on this row are filled...
                if (filledTiles == Width)
                {
                    linesCleared++;
                    // Set the row to empty tiles
                    for (int x = 0; x < Width; x++)
                    {
                        Tiles[x, y] = -1;
                    }
                }
            }
            if (linesCleared > 0)
            {
                Assets.LinesClear.Play();
                if (_game.LevelManager.AutoIncrement) {
                    if (linesCleared <= 2) {
                        _game.LevelManager.Increase(linesCleared);
                    } else if (linesCleared == 3) {
                        _game.LevelManager.Increase(4);
                    } else if (linesCleared == 4) {
                        _game.LevelManager.Increase(6);
                        Assets.TetrisClear.Play();
                    } else {
                        throw new System.Exception("Cleared more than 4 lines at once, should not be possible.");
                    }
                }
                LineClearFramesToWait = _game.LevelManager.LineClear;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Shifts any floating rows down. Should be called after LineClear frames after clearing lines.
        /// </summary>
        public void ShiftRowsDown()
        {
            int rowsShifted = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int y = Height - 1; y > 0; y--)
                {
                    int emptyTiles = 0;
                    for (int x = 0; x < Width; x++)
                    {
                        if (Tiles[x, y] == -1) emptyTiles++;
                    }

                    if (emptyTiles == Width)
                    {
                        rowsShifted++;
                        for (int sy = y; sy > 0; sy--)
                        {
                            for (int sx = 0; sx < Width; sx++)
                            {
                                Tiles[sx, sy] = Tiles[sx, sy - 1];
                            }
                        }
                    }
                }
            }
            if (rowsShifted > 0) {
                Assets.LinesFall.Play();
            }
        }
        public void Update(GameTime gameTime)
        {
            if (LineClearFramesToWait > 0)
            {
                LineClearFramesToWait--;
                // If we just now hit 0 frames, shift the lines down
                if (LineClearFramesToWait == 0)
                {
                    ShiftRowsDown();
                }
                return;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw black background
            spriteBatch.Draw(Assets.Pixel, new Rectangle((int)Offset.X, (int)Offset.Y + _invisibleRows * TetrisGame.TILE_SIZE, Width  * TetrisGame.TILE_SIZE, (Height - _invisibleRows) * TetrisGame.TILE_SIZE), Color.Black);
            // Draw tiles
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int id = Tiles[x, y];
                    if (id >= 0 && id < 7)
                    {
                        // Draw the tile based on the piece colour
                        spriteBatch.Draw(Assets.BlockW[id], Utils.RectangleF(Offset + new Vector2(x, y) * TetrisGame.TILE_SIZE, new Vector2(TetrisGame.TILE_SIZE)), Color.White);
                    }
                    
                }
            }
        }
    }
}
