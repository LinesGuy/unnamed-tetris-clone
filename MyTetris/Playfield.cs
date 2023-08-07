using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.CodeAnalysis;

namespace MyTetris
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
            // Draw playfield
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int id = Tiles[x, y];
                    if (id >= 0 && id < 7)
                    {
                        // Draw the tile based on the piece colour
                        spriteBatch.Draw(Assets.TileBlank_32, Offset + new Vector2(x, y) * TetrisGame.TILE_SIZE, null, PieceData.Colours[id], 0f, Vector2.Zero, 1f, 0, 0);
                    } else // Draw a background tile
                    {
                        // Only draw the background if we aren't drawing an invisible row
                        if (y < _invisibleRows)
                            continue;
                        spriteBatch.Draw(Assets.TileBlank_32, Offset + new Vector2(x, y) * TetrisGame.TILE_SIZE, null, new Color(64, 64, 64), 0f, Vector2.Zero, 1f, 0, 0);
                    }
                }
            }
        }
    }
}
