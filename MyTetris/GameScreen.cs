using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public class GameScreen : Screen
    {
        private TetrisGame _game = new TetrisGame();
        public override void Update(GameTime gameTime)
        {
            if (InputManager.WasKeyJustDown(Keys.Escape))
            {
                ScreenManager.Screens.Pop();
            }
            _game.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            _game.Draw(spriteBatch);
        }
    }
}
