using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MyTetris
{
    public class ButtonGroup
    {
        public List<Button> Buttons = new List<Button>();
        private int _selectedIndex = 0;
        private Vector2 _position;
        public ButtonGroup(Vector2 position)
        {
            _position = position;
        }
        public void Update()
        {
            if (InputManager.WasKeyJustDown(Keys.Up))
            {
                _selectedIndex--;
                if (_selectedIndex < 0 )
                {
                    _selectedIndex = Buttons.Count - 1;
                }
            }

            if (InputManager.WasKeyJustDown(Keys.Down))
            {
                _selectedIndex++;
                if (_selectedIndex >= Buttons.Count)
                {
                    _selectedIndex = 0;
                }
            }
            if (InputManager.WasKeyJustDown(Keys.Space) || InputManager.WasKeyJustDown(Keys.Enter))
            {
                Buttons[_selectedIndex].OnClick();
            };
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                // Draw position based on index so the group appears as a vertical list
                // Colour is white by default but red if it is the currently selected button
                Buttons[i].Draw(spriteBatch, _position + new Vector2(0, i * 30), i == _selectedIndex ? Color.Red : Color.White);
            }
        }
    }
}
