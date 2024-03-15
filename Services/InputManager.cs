using EC.Components.Colliders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
	/// <summary>
	/// Manages the input from keyboard and mouse, providing methods to check the current and previous states of input devices.
	/// This class helps in detecting various input actions like key presses, key releases, and mouse movements.
	/// </summary>
	public class InputManager : GameComponent, IService
    {
        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;

        private MouseState previousMouseState;
        private MouseState currentMouseState;

		public MouseState PreviousMouseState => previousMouseState;
		public MouseState CurrentMouseState => currentMouseState;

        private DisplayManager displayManager;

		private bool isMousePressedWithinBounds = false;
		private Dictionary<Collider2D, bool> mousePressedStates = new Dictionary<Collider2D, bool>();
		
		/// <summary>
		/// Initializes a new instance of the InputManager class.
		/// </summary>
		/// <param name="displayManager">The display manager used to help manage input relative to the game display.</param>
		/// <param name="game">The game instance this component belongs to.</param>
		public InputManager(DisplayManager displayManager, Game game) : base(game)
        {
			this.displayManager = displayManager;
        }

		/// <summary>
		/// Updates the input states for keyboard and mouse. This method should be called every frame to update the input state.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			previousKeyboardState = currentKeyboardState;
			currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
		}

		/// <summary>
		/// Checks if the specified key is currently being held down.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>True if the key is held down; otherwise, false.</returns>
		public bool KeyHeld(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

		/// <summary>
		/// Checks if the specified key was just pressed in the current frame.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>True if the key was just pressed; otherwise, false.</returns>
		public bool KeyJustPressed(Keys key)
        {
            return (previousKeyboardState.IsKeyUp(key) &&
                    currentKeyboardState.IsKeyDown(key));
        }

		/// <summary>
		/// Checks if the specified key is currently up (not pressed).
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns>True if the key is up; otherwise, false.</returns>
		public bool KeyUpCurrently(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

		/// <summary>
		/// Checks if the specified key was just released in the current frame.
		/// </summary>
		/// <param name="keys">The key to check.</param>
		/// <returns>True if the key was just released; otherwise, false.</returns>
		public bool KeyJustUp(Keys keys)
        {
			if (mousePressedStates.Values.Any())
				mousePressedStates.Clear();

            return previousKeyboardState.IsKeyDown(keys) && currentKeyboardState.IsKeyUp(keys);
        }

		/// <summary>
		/// Checks if the left mouse button is currently being held down.
		/// </summary>
		/// <returns>True if the left mouse button is held down; otherwise, false.</returns>
		public bool MouseButtonHeld()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed;
        }

		/// <summary>
		/// Checks if the left mouse button was just pressed in the current frame.
		/// </summary>
		/// <returns>True if the left mouse button was just pressed; otherwise, false.</returns>
		public bool MouseJustPressed()
        {
            return (previousMouseState.LeftButton == ButtonState.Released) &&
                   (currentMouseState.LeftButton == ButtonState.Pressed);
        }

		/// <summary>
		/// Checks if the left mouse button is currently up (not pressed).
		/// </summary>
		/// <returns>True if the left mouse button is up; otherwise, false.</returns>
		public bool MouseUpCurrently()
        {
            return currentMouseState.LeftButton == ButtonState.Released;
        }

		/// <summary>
		/// Checks if the left mouse button was just released in the current frame.
		/// </summary>
		/// <returns>True if the left mouse button was just released; otherwise, false.</returns>
		public bool MouseButtonJustUp()
        {
            return (previousMouseState.LeftButton == ButtonState.Pressed) &&
                   (currentMouseState.LeftButton == ButtonState.Released);
        }

		/// <summary>
		/// Determines if a complete mouse click (press and release) occurred within the specified collider's bounds.
		/// This method checks whether the mouse was pressed and released within the same collider bounds, 
		/// ensuring that the full click action is contained within the collider.
		/// </summary>
		/// <param name="collider2D">The collider to check for mouse click containment. Can be either BoxCollider2D or CircleCollider2D.</param>
		/// <returns>True if the mouse was pressed and released within the bounds of the specified collider; otherwise, false.</returns>
		public bool HasFullyClickedInBounds(Collider2D collider2D)
		{
			// Initialize state for the collider if not already present
			if (!mousePressedStates.ContainsKey(collider2D))
			{
				mousePressedStates[collider2D] = false;
			}

			// Check for mouse down within bounds
			if (MouseJustPressed() && IsMouseWithinCollider(collider2D))
			{
				mousePressedStates[collider2D] = true;

			}

			// Check for mouse up within bounds
			if (MouseButtonJustUp())
			{
				if (mousePressedStates[collider2D] && IsMouseWithinCollider(collider2D))
				{
					// Reset state and return true for a complete click
					mousePressedStates[collider2D] = false;
					return true;
				}

				// Reset state if mouse was not released within the same bounds
				mousePressedStates[collider2D] = false;
			}

			return false;
		}

		private bool IsMouseWithinCollider(Collider2D collider2D)
		{
			var mousePosition = Mouse.GetState().Position.ToVector2();
			mousePosition = displayManager.ConvertWindowToViewport(mousePosition);
			return (collider2D is BoxCollider2D boxCollider && boxCollider.Bounds.Contains(mousePosition)) ||
				   (collider2D is CircleCollider2D circleCollider && circleCollider.Bounds.Contains(mousePosition));
		}

		/// <summary>
		/// Gets the current position of the mouse cursor.
		/// </summary>
		/// <returns>The current position of the mouse cursor as a Vector2.</returns>
		public Vector2 MousePosition()
        {
			
            return displayManager.ConvertWindowToViewport(currentMouseState.Position.ToVector2());
        }

		/// <summary>
		/// Checks if the mouse cursor is currently within the screen boundaries.
		/// </summary>
		/// <returns>True if the mouse is on the screen; otherwise, false.</returns>
		public bool MouseOnScreen()
        {
            if (MousePosition().X > 0 && MousePosition().X < displayManager.WindowSize.X &&
                MousePosition().Y > 0 && MousePosition().Y < displayManager.WindowSize.Y)
                return true;

            return false;   
        }

	}
}
