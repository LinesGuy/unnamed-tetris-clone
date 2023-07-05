using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public static class Utils
    {
        public static void DrawStringCentered(this SpriteBatch batch, SpriteFont font, string text, Vector2 position, Color color) => batch.DrawString(font, text, position, color, 0f, font.MeasureString(text) / 2f, 1f, 0, 0);
    }
}
