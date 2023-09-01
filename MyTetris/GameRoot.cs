using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyTetris
{
    public class GameRoot : Game
    {
        public static GameRoot Instance;
        public GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;
        public Point ScreenSize = new Point(1366, 768);
        public GameRoot()
        {
            Instance = this;
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = ScreenSize.X;
            Graphics.PreferredBackBufferHeight = ScreenSize.Y;
            Graphics.ApplyChanges();
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
            _spriteBatch = new SpriteBatch(base.GraphicsDevice);
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
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            ScreenManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}