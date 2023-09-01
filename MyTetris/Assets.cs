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
        public static Texture2D[] BlockW;
        public static SoundEffect Ready;
        public static SoundEffect Go;
        public static SoundEffect PieceSlide;
        public static SoundEffect PieceLock;
        public static SoundEffect LinesClear;
        public static SoundEffect LinesFall;
        public static SoundEffect TetrisClear;
        public static SoundEffect[] Mino;
        public static void Load(ContentManager content)
        {
            NovaSquare24 = content.Load<SpriteFont>("Fonts/NovaSquare24");
            NovaSquare48 = content.Load<SpriteFont>("Fonts/NovaSquare48");
            Nullpomino = content.Load<SpriteFont>("Fonts/Nullpomino");
            Pixel = content.Load<Texture2D>("Textures/Pixel");
            List<Texture2D> BlockWList = new List<Texture2D>();
            for (int i = 0; i < 7; i++) {
                BlockWList.Add(content.Load<Texture2D>($"Textures/BlockW{i}"));
            }
            BlockW = BlockWList.ToArray();
            Ready = content.Load<SoundEffect>("SoundEffects/SEP_ready");
            Go = content.Load<SoundEffect>("SoundEffects/SEP_go");
            PieceSlide = content.Load<SoundEffect>("SoundEffects/SEB_instal");
            PieceLock = content.Load<SoundEffect>("SoundEffects/SEB_fixa");
            LinesClear = content.Load<SoundEffect>("SoundEffects/SEB_disappear");
            LinesFall = content.Load<SoundEffect>("SoundEffects/SEB_fall");
            TetrisClear = content.Load<SoundEffect>("SoundEffects/SEP_tetris");
            List<SoundEffect> MinoList = new List<SoundEffect>();
            for (int i = 0; i < 7; i++) {
                MinoList.Add(content.Load<SoundEffect>($"SoundEffects/mino{i}"));
            }
            Mino = MinoList.ToArray();
        }
    }
}
