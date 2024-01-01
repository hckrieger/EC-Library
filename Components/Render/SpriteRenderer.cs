using EC.Components.Renderers;
using EC.CoreSystem;
using EC.Services;
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
		

		/// <summary>
		/// Initializes a new instance of the SpriteRenderer class.
		/// </summary>
		/// <param name="texturePath">The path to the texture for the sprite.</param>
		/// <param name="game">The game instance this renderer belongs to.</param>
		/// <param name="entity">The entity this renderer is associated with.</param>
		public SpriteRenderer(string texturePath, Game game, Entity entity) : base(game, entity)
		{
			textureName = texturePath;
			Texture = renderManager.GraphicsAssetManager.LoadSprite(texturePath);
		}



	}
}
