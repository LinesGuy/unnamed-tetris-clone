using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MyTetris
{
    public class ButtonGroup
    {
        public List<Button> Buttons = new List<Button>();
        private int _highlightedIndex = 0;
        private Vector2 _position;
        private Keys _upKey;
        private Keys _downKey;
        public ButtonGroup(Vector2 position, Keys up = Keys.Up, Keys down = Keys.Down, Keys left = Keys.Left, Keys right = Keys.Right, Keys select = Keys.Enter)
        {
            _upKey = up;
            _downKey = down;
            _position = position;
        }
        public void Update()
        {
            if (InputManager.WasKeyJustDown(_upKey))
            {
                _highlightedIndex--;
                if (_highlightedIndex < 0 )
                {
                    _highlightedIndex = Buttons.Count - 1;
                }
            }

            if (InputManager.WasKeyJustDown(_downKey))
            {
                _highlightedIndex++;
                if (_highlightedIndex >= Buttons.Count)
                {
                    _highlightedIndex = 0;
                }
            }
            Buttons[_highlightedIndex].HighlightedUpdate?.Invoke();
            Buttons.ForEach(b => b.Update?.Invoke());
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                // Draw position based on index so the group appears as a vertical list
                // Colour is white by default but red if it is the currently selected button
                Buttons[i].Draw(spriteBatch, _position + new Vector2(0, i * 30), i == _highlightedIndex ? Color.Red : Color.White);
            }
        }
    }
}
