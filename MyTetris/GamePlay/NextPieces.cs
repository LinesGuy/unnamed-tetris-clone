﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris.GamePlay
{
    public class NextPieces
    {
        private TetrisGame _game;
        private BagRandomizer _bag = new BagRandomizer();
        public Queue<int> Pieces = new Queue<int>();
        public NextPieces(TetrisGame game)
        {
            _game = game;
            for (int i = 0; i < 3; i++)
            {
                Pieces.Enqueue(_bag.Next());
            }
        }
        public int Next()
        {
            Pieces.Enqueue(_bag.Next());
            return Pieces.Dequeue();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            int x_offset = 3;
            foreach (int piece in Pieces)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (PieceData.Tiles[piece, 0][x + y * 4] == '0') continue;
                        spriteBatch.Draw(Assets.BlockW[piece], Utils.RectangleF(_game.Position + new Vector2(x + x_offset, y) * TetrisGame.TILE_SIZE, new Vector2(TetrisGame.TILE_SIZE)), Color.White);
                    }
                }
                x_offset += 5;
            }
        }
    }
}
