using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyTetris
{
    public class GameRoot : Game
    {
        public static GameRoot Instance;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Point ScreenSize = new Point(1366, 768);
        public GameRoot()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = ScreenSize.X;
            _graphics.PreferredBackBufferHeight = ScreenSize.Y;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            ScreenManager.Screens.Push(new TitleScreen());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();
            ScreenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            ScreenManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}