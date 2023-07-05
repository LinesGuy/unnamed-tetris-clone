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
        public override void Draw(SpriteBatch spriteBatch)
        {
            Utils.DrawStringCentered(spriteBatch, Assets.NovaSquare48, "the game", new Vector2(200, 200), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.WasKeyJustDown(Keys.Escape))
            {
                ScreenManager.Screens.Pop();
            }
        }
    }
}
