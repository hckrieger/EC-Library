using EC.CoreSystem;
using EC.Services;
using EC.Services.AssetManagers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components
{
	public class RectangleRenderer : Component
	{
		readonly private RenderManager renderManager;
		private Transform transform;
		private float layerDepth;
		private int width, height;
		private Color color;
		private string name;

		public int TextureWidth => width;
		public int TextureHeight => height;

		public Color Color 
		{
			get { return color; }	
			set { color = value; }
		}

		private Transform Transform
		{
			get
			{
				if (transform == null)
					transform = Entity?.GetComponent<Transform>();
				return transform;
			}
		}

		private Vector2 Origin
		{
			get
			{
				var originComponent = Entity?.GetComponent<Origin>();

				if (originComponent == null)
					return Vector2.Zero;

				return originComponent.Value;
			}
		}

		public float LayerDepth
		{
			get { return layerDepth; }
			set { layerDepth = value; }
		}


		public RectangleRenderer(string name, int width, int height, Color color, Game game, Entity entity) : base(entity)
		{
			renderManager = game.Services.GetService<RenderManager>();
			this.width = width;
			this.height = height;
			this.color = color;
			this.name = name;
			layerDepth = .5f;
		}


		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			renderManager.DrawRectangle(name,
										Transform?.Position ?? Vector2.Zero, 
									    width, height, Color,
										Origin,
										transform.Rotation, transform.Scale, layerDepth); 
		}
	}
}
