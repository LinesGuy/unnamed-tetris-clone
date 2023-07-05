using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public static class Assets
    {
        public static SpriteFont NovaSquare24;
        public static SpriteFont NovaSquare48;
        public static SpriteFont Nullpomino;
        public static Texture2D Pixel;
        public static void Load(ContentManager content)
        {
            Pixel = content.Load<Texture2D>("Textures/Pixel");
            NovaSquare24 = content.Load<SpriteFont>("Fonts/NovaSquare24");
            NovaSquare48 = content.Load<SpriteFont>("Fonts/NovaSquare48");
            Nullpomino = content.Load<SpriteFont>("Fonts/Nullpomino");
        }
    }
}
