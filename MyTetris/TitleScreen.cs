using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public class TitleScreen : Screen
    {

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Assets.NovaSquare48, "My Tetris", new Vector2(50, 50), Color.White);
            var asdf = () => { Debug.WriteLine("a"); };
        }
        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine("meow");
        }
    }
}
