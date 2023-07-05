using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    public static class InputManager
    {
        public static KeyboardState Keyboard;
        public static KeyboardState LastKeyboard;
        public static void Update()
        {
            LastKeyboard = Keyboard;
            Keyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }
        public static bool WasKeyJustDown(Keys key) => Keyboard.IsKeyDown(key) && LastKeyboard.IsKeyUp(key);
        public static bool WasKeyJustUp(Keys key) => Keyboard.IsKeyUp(key) && LastKeyboard.IsKeyDown(key);
    }
}
