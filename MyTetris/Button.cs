using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public class Button
    {
        public string Text;
        public Action OnClick;
        public Button(string text, Action onClick)
        {
            Text = text;
            OnClick = onClick;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 offset, Color color)
        {
            spriteBatch.DrawString(Assets.NovaSquare24, Text, offset, color);
        }
    }
}
