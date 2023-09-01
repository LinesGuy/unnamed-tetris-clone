using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris {
    public class WindowManager {
        public List<Window> Windows = new List<Window>();
        private Window _draggedWindow;
        public void Update(GameTime gameTime) {
            // If user left clicked, see if we are hovering over any windows
            if (InputManager.WasLeftMouseJustDown) {
                foreach(Window window in Windows) {
                    if (window.Rect.Contains(InputManager.Mouse.Position)) {
                        _draggedWindow = window;
                        break;
                    }
                }
            }
            // If user released left click and we are holding a window
            if (InputManager.WasLeftMouseJustUp) {
                _draggedWindow = null;
            }
            if (_draggedWindow != null) {
                _draggedWindow.Rect.Location += InputManager.Mouse.Position - InputManager.LastMouse.Position;
            }
            Windows.ForEach(w => w.Update(gameTime));
        }
        public void Draw(SpriteBatch spriteBatch) {
            foreach(Window window in Windows) {
                // Draw BG
                spriteBatch.Draw(Assets.Pixel, new Rectangle(window.Rect.Location - new Point(1), window.Rect.Size + new Point(2)), Color.White);
                // Draw window contents
                GameRoot.Instance.GraphicsDevice.SetRenderTarget(window.RenderTarget);
                SpriteBatch batch = new SpriteBatch(GameRoot.Instance.GraphicsDevice);
                batch.Begin(samplerState: SamplerState.PointClamp);
                window.Draw(batch);
                batch.End();
                // Draw window onto main screen
                spriteBatch.Draw(window.RenderTarget, window.Rect, Color.White);
                GameRoot.Instance.GraphicsDevice.SetRenderTarget(null);
            }
        }
    }
}
