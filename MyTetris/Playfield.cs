using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public class Playfield
    {
        private int[,] _tiles;
        private int _width = 10;
        // 24 rows minus 4 invisible rows means only the bottom 20 rows will be visible to the player, as is somewhat standard in Tetris
        private int _height = 24;
        private int _invisibleRows = 4;
        private Vector2 _position = new Vector2(50, 50);
        const float TILE_SIZE = 32;
        public Playfield()
        {
            _tiles = new int[_width, _height];
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _tiles[x, y] = -1;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = _invisibleRows; y < _height; y++)
            {
                for (int x = 0; x < _width; x++) {
                    switch(_tiles[x, y])
                    {
                        case -1:
                            spriteBatch.Draw(Assets.TileBlank_32, _position + new Vector2(x, y - _invisibleRows) * TILE_SIZE, null, Color.White, 0f, Vector2.Zero, 1f, 0, 0);
                            break;
                        default:
                            throw new Exception("Invalid tile ID");
                    }
                    
                }
            }
        }
    }
}
