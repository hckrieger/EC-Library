using EC.Components;
using EC.Components.Colliders;
using EC.Components.Render;
using EC.CoreSystem;
using EC.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
	public class FactoryManager
	{
		public enum ColliderShape
		{
			Rectangle,
			Circle,
			None
		}
		public Entity CreateRectangleEntity(string name, int width, int height, Color color, Game game, bool addCollider = true)
		{
			// Create a basic entity with a Transform and RectangleRenderer
			Entity entity = new Entity(game);
			entity.AddComponent(new Transform(entity));
			entity.AddComponent(new RectangleRenderer(name, width, height, color, game, entity));

			if (addCollider)
			{
				entity.AddComponent(new BoxCollider2D(new Rectangle(0, 0, width, height), entity));
			}

			return entity;
		}

		public Entity CreateSpriteEntity(string name, Game game, ColliderShape colliderShape = ColliderShape.None)
		{
			Entity entity = new Entity(game);
			entity.AddComponent(new Transform(entity));
			SpriteRenderer spriteRenderer = new SpriteRenderer(name, game, entity);
			entity.AddComponent(spriteRenderer);

			switch (colliderShape)
			{
				case ColliderShape.Rectangle:
					entity.AddComponent(new BoxCollider2D(new Rectangle(0, 0, spriteRenderer.TextureWidth, spriteRenderer.TextureHeight), entity));
					break;
				case ColliderShape.Circle:
					//entity.AddComponent(new Origin(new Vector2(spriteRenderer.TextureWidth / 2, spriteRenderer.TextureHeight / 2), entity));
					int radius = Math.Min(spriteRenderer.TextureWidth, spriteRenderer.TextureHeight);
					entity.AddComponent(new CircleCollider2D(new Circle(new Vector2(spriteRenderer.TextureWidth/2, spriteRenderer.TextureHeight/2), radius), entity));
					break;
				case ColliderShape.None:
				    break;
				default:
					throw new ArgumentException("Invalid collider shape");
			}
		}
	}
}
