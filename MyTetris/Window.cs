using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyTetris {
    public class Window {
        public Rectangle Rect;
        public RenderTarget2D RenderTarget;
        public Window(Rectangle rect) {
            Rect = rect;
            RenderTarget = new RenderTarget2D(GameRoot.Instance.GraphicsDevice, rect.Width, rect.Height);
        }
        public virtual void Update(GameTime gameTime) {

        }
        public virtual void Draw(SpriteBatch spriteBatch) {
            
        }
    }
}
