using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyTetris.GamePlay;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris {
    public class QuickSettingsWindow : Window {
        private TetrisGame _game;
        ButtonGroup buttonGroup = new ButtonGroup(new Vector2(5), Keys.NumPad8, Keys.NumPad2, Keys.NumPad4, Keys.NumPad6, Keys.Enter);
        public QuickSettingsWindow(TetrisGame game, Rectangle rect) : base(rect) {
            _game = game;
            
            Button speedLevelButton = new Button("< SpeedLevel: null >");
            speedLevelButton.Update = () => { speedLevelButton.Text = $"< SpeedLevel: {_game.LevelManager.SpeedLevel} >"; };
            speedLevelButton.HighlightedUpdate = () => {
                if (InputManager.WasKeyJustDown(Keys.NumPad4)) { _game.LevelManager.Increase(-100); }
                if (InputManager.WasKeyJustDown(Keys.NumPad6)) { _game.LevelManager.Increase(100); }
            };
            buttonGroup.Buttons.Add(speedLevelButton);
        }
        public override void Update(GameTime gameTime) {
            buttonGroup.Update();
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch) {
            //spriteBatch.Draw(Assets.TileBlank_16, Vector2.Zero, Color.White);
            //spriteBatch.DrawString(Assets.NovaSquare24, "asdf", Vector2.Zero, Color.White);
            buttonGroup.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
