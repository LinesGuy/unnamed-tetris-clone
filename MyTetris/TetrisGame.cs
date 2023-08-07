﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public class TetrisGame
    {
        public Hold Hold;
        public PlayField PlayField;
        public NextPieces NextPieces;
        public StatsManager StatsManager;
        public CurrentPiece CurrentPiece;
        public LevelManager LevelManager;
        public Vector2 Position = new Vector2(50, 50); // The top-left of the visible playfield
        public const float TILE_SIZE = 32f;
        public TetrisGame() {
            Hold = new Hold(this);
            PlayField = new PlayField(this);
            NextPieces = new NextPieces(this);
            StatsManager = new StatsManager(this);
            CurrentPiece = new CurrentPiece(this);
            LevelManager = new LevelManager(this);
        }
        public void Update(GameTime gameTime) {
            PlayField.Update(gameTime);
            CurrentPiece.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            PlayField.Draw(spriteBatch);
            CurrentPiece.Draw(spriteBatch);
            NextPieces.Draw(spriteBatch);
            StatsManager.Draw(spriteBatch);
            Hold.Draw(spriteBatch);
        }
    }
}
