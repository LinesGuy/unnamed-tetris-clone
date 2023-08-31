using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyTetris.GamePlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public class GameScreen : Screen
    {
        public TetrisGame Game = new TetrisGame();
        public WindowManager WindowManager = new WindowManager();
        public GameScreen() {
            WindowManager.Windows.Add(new QuickSettingsWindow(Game, new Rectangle(600, 400, 600, 700)));
        }
        public override void Update(GameTime gameTime)
        {
            if (InputManager.WasKeyJustDown(Keys.Escape))
            {
                ScreenManager.Screens.Pop();
            }
            Game.Update(gameTime);
            WindowManager.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Assets.Pixel, new Rectangle(Point.Zero, GameRoot.Instance.ScreenSize), Color.CornflowerBlue);
            Game.Draw(spriteBatch);
            WindowManager.Draw(spriteBatch);
        }
    }
}
