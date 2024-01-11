using EC.Components;
using EC.Components.Colliders;
using EC.Components.Render;
using EC.Components.UI;
using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utilities.Extensions
{
	/// <summary>
	/// Provides extension methods for the Entity class for adding various components.
	/// </summary>
	public static class EntityExtensions
	{
		public enum ColliderShape
		{
			Rectangle,
			Circle,
			None
		}

		/// <summary>
		/// Loads sprite components into the given entity, including a sprite renderer and an optional collider.
		/// </summary>
		/// <param name="entity">The entity to which the components will be added.</param>
		/// <param name="name">The name identifier for the sprite renderer.</param>
		/// <param name="game">The current game instance.</param>
		/// <param name="colliderShape">The shape of the collider to be added. Defaults to None.</param>
		/// <exception cref="ArgumentException">Thrown when an invalid collider shape is specified.</exception>
		public static void LoadSpriteComponents(this Entity entity, string name, Game game, ColliderShape colliderShape = ColliderShape.None)
		{
			entity.AddComponent(new Transform(entity));

			SpriteRenderer spriteRenderer = new SpriteRenderer(name, game, entity);
			entity.AddComponent(spriteRenderer);

			entity.AddComponent(new Origin(Vector2.Zero, entity));

			switch (colliderShape)
			{
				case ColliderShape.Rectangle:
					entity.AddComponent(new BoxCollider2D(new Rectangle(0, 0, spriteRenderer.TextureWidth, spriteRenderer.TextureHeight), entity));
					break;
				case ColliderShape.Circle:
					entity.GetComponent<Origin>().Value = spriteRenderer.TextureCenter;
					int radius = Math.Min(spriteRenderer.TextureWidth, spriteRenderer.TextureHeight);

					var transform = entity.GetComponent<Transform>();
					var origin = entity.GetComponent<Origin>();
					var vector = new Vector2(transform.Position.X + origin.Value.X, transform.Position.Y + origin.Value.Y);
					entity.AddComponent(new CircleCollider2D(new Circle(vector, radius), entity));
					break;
				case ColliderShape.None:
					break;
				default:
					throw new ArgumentException("Invalid collider shape");
			}

		}

		/// <summary>
		/// Loads a rectangle texture and an optional collider into the given entity.
		/// </summary>
		/// <param name="entity">The entity to which the texture and collider will be added.</param>
		/// <param name="name">The name identifier for the texture renderer.</param>
		/// <param name="width">Width of the rectangle texture.</param>
		/// <param name="height">Height of the rectangle texture.</param>
		/// <param name="color">The color of the rectangle texture.</param>
		/// <param name="game">The current game instance.</param>
		/// <param name="addCollider">Specifies whether to add a collider to the entity. Defaults to false.</param>
		public static void LoadRectangleTextureComponents(this Entity entity, string name, int width, int height, Color color, Game game, bool addCollider = false)
		{
			entity.AddComponent(new Transform(entity));
			entity.AddComponent(new RectangleRenderer(name, width, height, color, game, entity));
			entity.AddComponent(new Origin(Vector2.Zero, entity));

			if (addCollider)
			{
				entity.AddComponent(new BoxCollider2D(new Rectangle(0, 0, width, height), entity));
			}
		}

		/// <summary>
		/// Loads a circle texture and an optional collider into the given entity.
		/// </summary>
		/// <param name="entity">The entity to which the texture and collider will be added.</param>
		/// <param name="name">The name identifier for the texture renderer.</param>
		/// <param name="radius">Radius of the circle texture.</param>
		/// <param name="color">The color of the circle texture.</param>
		/// <param name="game">The current game instance.</param>
		/// <param name="addCollider">Specifies whether to add a collider to the entity. Defaults to false.</param>
		public static void LoadCircleTextureComponents(this Entity entity, string name, int radius, Color color, Game game, bool addCollider = false)
		{
			entity.AddComponent(new Transform(entity));
			entity.AddComponent(new CircleRenderer(name, radius, color, game, entity));
			entity.AddComponent(new Origin(entity.GetComponent<CircleRenderer>().TextureCenter, entity));	

			if (addCollider)
			{
				var transform = entity.GetComponent<Transform>();
				var origin = entity.GetComponent<Origin>();
				var vector = new Vector2(transform.Position.X + origin.Value.X, transform.Position.Y + origin.Value.Y);

				entity.AddComponent(new CircleCollider2D(new Circle(vector, radius), entity));
			}
		}

	}
}
