using Microsoft.Xna.Framework.Audio;
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
        public static Texture2D TileBlank_16;
        public static Texture2D TileBlank_32;
        public static SoundEffect Ready;
        public static void Load(ContentManager content)
        {
            NovaSquare24 = content.Load<SpriteFont>("Fonts/NovaSquare24");
            NovaSquare48 = content.Load<SpriteFont>("Fonts/NovaSquare48");
            Nullpomino = content.Load<SpriteFont>("Fonts/Nullpomino");
            Pixel = content.Load<Texture2D>("Textures/Pixel");
            TileBlank_16 = content.Load<Texture2D>("Textures/TileBlank_16");
            TileBlank_32 = content.Load<Texture2D>("Textures/TileBlank_32");
            Ready = content.Load<SoundEffect>("SoundEffects/SEP_ready");
        }
    }
}
