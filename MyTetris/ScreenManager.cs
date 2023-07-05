using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public static class ScreenManager
    {
        public static Stack<Screen> Screens = new Stack<Screen>();
        public static void Update(GameTime gameTime)
        {
            Screens.Peek().Update(gameTime);
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            Screens.Peek().Draw(spriteBatch);
        }
    }
}
