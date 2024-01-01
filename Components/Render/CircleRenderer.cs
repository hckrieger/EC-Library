using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Render
{
	public class CircleRenderer : TextureRenderer
	{
		private int radius;

		/// <summary>
		/// Initializes a new instance of the CircleRenderer class.
		/// </summary>
		/// <param name="circleTextureName">The name of the texture for the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
		/// <param name="color">The color of the circle.</param>
		/// <param name="game">The game instance which this renderer belongs to.</param>
		/// <param name="entity">The entity to which this renderer is attached.</param>
		public CircleRenderer(string circleTextureName, int radius, Color color, Game game, Entity entity) : base(game, entity)
		{
			this.radius = radius;
			Color = color;
			Texture = renderManager.GraphicsAssetManager.LoadCircle(circleTextureName, radius);
		}


	}
}
