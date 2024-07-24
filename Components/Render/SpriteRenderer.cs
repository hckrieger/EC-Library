using EC.Components.Colliders;
using EC.Components.Renderers;
using EC.CoreSystem;
using EC.Services;
using EC.Services.AssetManagers;
using EC.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Render
{
	/// <summary>
	/// Renderer for drawing sprites. This can be used for characters, objects, or any other sprite-based graphics.
	/// </summary>
	public class SpriteRenderer : TextureRenderer
	{
		private GraphicsAssetManager graphicsAssetManager;
		private Game game;
		private Entity entity;
		private string texturePath;
		private Point gridSize;
		private int gridIndex;

		/// <summary>
		/// Initializes a new instance of the SpriteRenderer class.
		/// </summary>
		/// <param name="texturePath">The path to the texture for the sprite.</param>
		/// <param name="game">The game instance this renderer belongs to.</param>
		/// <param name="entity">The entity this renderer is associated with.</param>
		public SpriteRenderer(string texturePath, Game game, Entity entity, Point gridSize = default, int gridIndex = 0) : base(game, entity)
		{
			TextureName = texturePath;
			
			graphicsAssetManager = game.Services.GetService<GraphicsAssetManager>();
			Texture = graphicsAssetManager.LoadSprite(texturePath);
			this.game = game;
			this.entity = entity;
			this.texturePath = texturePath;
			this.gridSize = gridSize;
			this.gridIndex = gridIndex;

			if (gridSize == default)
				gridSize = new Point(TextureWidth, TextureHeight);

			
			
		}

		public override void Initialize()
		{
			base.Initialize();
			SetSpriteFrame(gridIndex);
		}

		public void SetSpriteFrame(int gridIndex)
		{
			SpriteSheet spriteSheet = graphicsAssetManager.LoadSpriteSheet(texturePath, entity, gridSize, gridIndex);

			SourceRectangle = spriteSheet.SourceRectangle;

			if (Entity.HasComponent<BoxCollider2D>())
				Entity.GetComponent<BoxCollider2D>().LocalBounds = new Rectangle(SourceRectangle.Value.X, SourceRectangle.Value.X, gridSize.X, gridSize.Y);
			else if (Entity.HasComponent<CircleCollider2D>())
			{
				int radius = Math.Min(gridSize.X, gridSize.Y);
				var center = Entity.GetComponent<Origin>().Value = new Vector2(SourceRectangle.Value.X + (gridSize.X / 2), SourceRectangle.Value.Y + (gridSize.Y / 2));	
				Entity.GetComponent<CircleCollider2D>().LocalBounds = new Circle(center, radius);
			}

		}
	}
}
