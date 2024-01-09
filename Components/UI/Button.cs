using EC.Components.Colliders;
using EC.Components.Render;
using EC.CoreSystem;
using EC.Services;
using EC.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.UI
{
	/// <summary>
	/// Represents a clickable button component that can be attached to an entity. The button uses collider components
	/// to detect click interactions and trigger actions when clicked.
	/// </summary>
	public class Button : Component
	{
		public event Action Clicked;
		private Collider2D collider2D;
		private Game game;
		private Entity entity;
		private InputManager inputManager;

		/// <summary>
		/// Initializes a new instance of the Button class, attaching it to an entity and linking it with the game's input system.
		/// </summary>
		/// <param name="game">The game instance this button belongs to. Used to access global services like InputManager.</param>
		/// <param name="entity">The entity that this button is attached to. The button uses the entity's collider component for click detection.</param>
		public Button(Game game, Entity entity) : base(entity)
		{
			
			inputManager = game.Services.GetService<InputManager>();	
			
				if (entity.HasComponent<BoxCollider2D>())
					collider2D = entity.GetComponent<BoxCollider2D>();
				else if (entity.HasComponent<CircleCollider2D>())
					collider2D = entity.GetComponent<CircleCollider2D>();
			

			this.game = game;
			this.entity = entity;
		}

		/// <summary>
		/// Update method to check for click interactions. If the button's collider is clicked, it triggers the Clicked event.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			if (collider2D != null && IsClicked())
			{
				OnClicked();
			}
		}


		/// <summary>
		/// Checks if the button has been clicked by determining if a mouse click occurred within its collider bounds.
		/// </summary>
		/// <returns>True if the button is clicked; otherwise, false.</returns>
		private bool IsClicked()
		{


			var mouseState = Mouse.GetState();
			Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

			if (collider2D is CircleCollider2D circleCollider)
			{
				return circleCollider.Bounds.Contains(mousePosition) && inputManager.MouseButtonJustUp();
			}
			else if (collider2D is BoxCollider2D boxCollider)
			{
				// Rectangle bounds check for other TextureRenderer types
				
				return boxCollider.Bounds.Contains(mousePosition) && inputManager.MouseButtonJustUp();
			}

			return false;
		}


		/// <summary>
		/// Invokes the Clicked event. This method can be overridden to implement custom click behavior.
		/// </summary>
		protected virtual void OnClicked()
		{
			Clicked?.Invoke();
		}
	}
}
