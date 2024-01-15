using EC.Components;
using EC.Components.Colliders;
using EC.Components.Render;
using EC.Components.UI;
using EC.CoreSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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


		/// <summary>
		/// Loads a font 
		/// </summary>
		/// <param name="entity">The entity to which the texture and collider will be added</param>
		/// <param name="path">The name identifier and path for the text renderer</param>
		/// <param name="text">The text that will be displayed</param>
		/// <param name="color">the color of the text</param>
		/// <param name="game">The current game instance</param>
		/// <param name="colliderShape"></param>
		public static void LoadTextComponents(this Entity entity, string path, string text, Color color, Game game, TextRenderer.Alignment alignment = TextRenderer.Alignment.Left)
		{
			entity.AddComponent(new Transform(entity));
			TextRenderer textRenderer = new TextRenderer(path, text, color, game, entity);
			textRenderer.TextAlignment = alignment;
			entity.AddComponent(textRenderer);

			//entity.AddComponent(new Origin(Vector2.Zero, entity));

			//switch (colliderShape != ColliderShape.None)
			//{
			//	case ColliderShape.Rectangle:
			//		var textSize = textRenderer.GetSize;
			//		entity.AddComponent(new BoxCollider2D(new Rectangle(0, 0, (int)textSize.X, (int)textSize.Y), entity));
			//		break;
			//	case ColliderShape.Circle:
			//		int radius = Math.Min((int)textRenderer.Width, (int)textRenderer.Height);

			//		var transform = entity.GetComponent<Transform>();
			//		var origin = entity.GetComponent<Origin>();
			//		var vector = new Vector2(transform.Position.X + origin.Value.X, transform.Position.Y + origin.Value.Y);
			//		entity.AddComponent(new CircleCollider2D(new Circle(transform.Position, radius), entity));
			//		break;
			//	case ColliderShape.None:
			//		// No collider added
			//		break;
			//	default:
			//		throw new ArgumentException("Invalid collider shape");
			//}

		}

	}
}
