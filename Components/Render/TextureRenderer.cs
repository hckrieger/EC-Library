using EC.Components.Renderers;
using EC.CoreSystem;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Render
{
	// Intermediate class for texture-based rendering
	public abstract class TextureRenderer : Renderer
	{
		protected string textureName;

		protected Texture2D Texture { get; set; }


		/// <summary>
		/// Gets the width fo the texture associated with this renderer. 
		/// </summary>
		public int TextureWidth => Texture?.Width ?? 0;

		/// <summary>
		/// Gets the height of the texture associated with this renderer.
		/// </summary>
		public int TextureHeight => Texture?.Height ?? 0;

		// ... texture-specific properties and methods ...

		public TextureRenderer(Game game, Entity entity) : base(game, entity)
		{
		}
	}
}
