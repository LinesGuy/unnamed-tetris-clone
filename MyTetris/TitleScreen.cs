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
        private ButtonGroup _buttons;
        public TitleScreen()
        {
            _buttons = new ButtonGroup(new Vector2(50, 200));
            _buttons.Buttons.Add(new Button("START") { HighlightedUpdate = () => ScreenManager.Screens.Push(new GameScreen()) });
            // TODO add options button here
            _buttons.Buttons.Add(new Button("EXIT") { HighlightedUpdate = () => { GameRoot.Instance.Exit(); } });
            Assets.Ready.Play();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Assets.NovaSquare48, "My Tetris", new Vector2(50, 50), Color.White);
            _buttons.Draw(spriteBatch);
        }
        public override void Update(GameTime gameTime)
        {
            _buttons.Update();
        }
    }
}
