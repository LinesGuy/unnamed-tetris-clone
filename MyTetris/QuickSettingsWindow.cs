using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyTetris.GamePlay;
using System;

namespace MyTetris {
    public class QuickSettingsWindow : Window {
        private TetrisGame _game;
        ButtonGroup buttonGroup = new ButtonGroup(new Vector2(5), Keys.NumPad8, Keys.NumPad2, Keys.NumPad4, Keys.NumPad6, Keys.Enter);
        public QuickSettingsWindow(TetrisGame game, Rectangle rect) : base(rect) {
            _game = game;
            // Change current speed level
            Button speedLevelButton = new Button("< SpeedLevel: ? >");
            speedLevelButton.HighlightedUpdate = () => {
                if (InputManager.WasKeyJustDown(Keys.NumPad4)) { _game.LevelManager.Increase(-100); }
                if (InputManager.WasKeyJustDown(Keys.NumPad6)) { _game.LevelManager.Increase(100); }
            };
            speedLevelButton.Update = () => { speedLevelButton.Text = $"< SpeedLevel: {_game.LevelManager.SpeedLevel} >"; };
            buttonGroup.Buttons.Add(speedLevelButton);

            // Change current DAS level and disable AutoIncrement
            Button dasButton = new Button("< DAS: ? >");
            dasButton.HighlightedUpdate = () => {
                if (InputManager.WasKeyJustDown(Keys.NumPad4)) {
                    _game.LevelManager.DAS = Math.Max(0, _game.LevelManager.DAS - 1);
                    _game.LevelManager.AutoIncrement = false;
                }
                if (InputManager.WasKeyJustDown(Keys.NumPad6)) {
                    _game.LevelManager.DAS++;
                    _game.LevelManager.AutoIncrement = false;
                }
            };
            dasButton.Update = () => { dasButton.Text = $"< DAS: {_game.LevelManager.DAS} >"; };
            buttonGroup.Buttons.Add(dasButton);

            // Toggle disabling auto piece locking
            Button lockButton = new Button("AutoPieceLock: ?");
            lockButton.HighlightedUpdate = () => {
                if (InputManager.WasKeyJustDown(Keys.NumPad4) || InputManager.WasKeyJustDown(Keys.NumPad6) || InputManager.WasKeyJustDown(Keys.Enter)) {
                    _game.CurrentPiece.AutoPieceLock = !_game.CurrentPiece.AutoPieceLock;
                }
            };
            lockButton.Update = () => { lockButton.Text = $"AutoPieceLock: {_game.CurrentPiece.AutoPieceLock}"; };
            buttonGroup.Buttons.Add(lockButton);

            // Toggle AutoIncrement
            Button levelIncrementToggle = new Button("Level Increment: ?");
            levelIncrementToggle.HighlightedUpdate = () => {
                if (InputManager.WasKeyJustDown(Keys.NumPad4) || InputManager.WasKeyJustDown(Keys.NumPad6) || InputManager.WasKeyJustDown(Keys.Enter)) {
                    _game.LevelManager.AutoIncrement = !_game.LevelManager.AutoIncrement;
                }
            };
            levelIncrementToggle.Update = () => {
                levelIncrementToggle.Text = $"Level Increment: {_game.LevelManager.AutoIncrement}";
            };
            buttonGroup.Buttons.Add(levelIncrementToggle);
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
