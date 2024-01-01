using EC.Components.Renderers;
using EC.CoreSystem;
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
	/// Renderer for drawing rectangular shapes.  THis can be used for UI elements, backgrounds, or any other rectangular graphics.
	/// </summary>
	public class RectangleRenderer : TextureRenderer
	{

		/// <summary>
		/// Initializes a new instance of the RectangleRenderer class.
		/// </summary>
		/// <param name="name">The name identifier for the texture.</param>
		/// <param name="width">The width of the rectangle.</param>
		/// <param name="height">The height of the rectangle.</param>
		/// <param name="color">The color of the rectangle.</param>
		/// <param name="game">The game instance this renderer belongs to.</param>
		/// <param name="entity">The entity this renderer is associated with.</param>
		public RectangleRenderer(string name, int width, int height, Color color, Game game, Entity entity) : base(game, entity)
		{
			textureName = name;
			Color = color;

			Texture = renderManager.GraphicsAssetManager.LoadRectangle(textureName, width, height);
		}


	}
}
