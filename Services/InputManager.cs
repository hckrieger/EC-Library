using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
    public class InputManager : GameComponent, IService
    {
        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;

        private MouseState previousMouseState;
        private MouseState currentMouseState;
        private DisplayManager displayManager;

        public InputManager(DisplayManager displayManager, Game game) : base(game)
        {
            this.displayManager = displayManager;
        }

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
		}

        public bool KeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool KeyJustPressed(Keys key)
        {
            return (previousKeyboardState.IsKeyUp(key) &&
                    currentKeyboardState.IsKeyDown(key));
        }

        public bool KeyUpCurrently(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        public bool KeyJustUp(Keys keys)
        {
            return previousKeyboardState.IsKeyDown(keys) && currentKeyboardState.IsKeyUp(keys);
        }

        public bool MouseButtonHeld()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool MouseJustPressed()
        {
            return (previousMouseState.LeftButton == ButtonState.Released) &&
                   (currentMouseState.LeftButton == ButtonState.Pressed);
        }

        public bool MouseUpCurrently()
        {
            return currentMouseState.LeftButton == ButtonState.Released;
        }

        public bool MouseButtonJustUp()
        {
            return (previousMouseState.LeftButton == ButtonState.Pressed) &&
                   (currentMouseState.LeftButton == ButtonState.Released);
        }

        public Vector2 MousePosition()
        {
            return currentMouseState.Position.ToVector2();
        }

        public bool MouseOnScreen()
        {
            if (MousePosition().X > 0 && MousePosition().X < displayManager.Width &&
                MousePosition().Y > 0 && MousePosition().Y < displayManager.Height)
                return true;

            return false;   
        }

	}
}
