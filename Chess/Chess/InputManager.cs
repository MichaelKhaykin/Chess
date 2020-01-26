using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public static class InputManager
    {
        public static MouseState Mouse;

        public static MouseState OldMouse;

        public static KeyboardState Keyboard;

        public static KeyboardState OldKeyboardState; 
        public static Vector2 ConvertGridIndexToPosition(int x, int y)
        {
            return new Vector2(x * Game1.SquareSize, y * Game1.SquareSize) + Game1.OffSet;
        }
    }
}
