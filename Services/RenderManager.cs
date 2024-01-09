using EC.Components;
using EC.Services.AssetManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
	/// <summary>
	/// Manages rendering operations in the game, providing methods to draw text and textures using a SpriteBatch.
	/// It simplifies the process of drawing by abstracting SpriteBatch calls and manages asset retrieval via the GraphicsAssetManager.
	/// </summary>
	public class RenderManager : IService
    {
		public GraphicsAssetManager GraphicsAssetManager { get; private set; }
  
        private SpriteBatch spriteBatch;

		/// <summary>
		/// Initializes a new instance of the RenderManager class.
		/// </summary>
		/// <param name="graphicsAssetManager">The GraphicsAssetManager to handle the retrieval of graphical assets.</param>
		/// <param name="spriteBatch">The SpriteBatch used for drawing operations.</param>
		public RenderManager(GraphicsAssetManager graphicsAssetManager, SpriteBatch spriteBatch)
        {
            GraphicsAssetManager = graphicsAssetManager;
            this.spriteBatch = spriteBatch;
        }

		/// <summary>
		/// Draws a string of text on the screen.
		/// </summary>
		/// <param name="spriteFont">The font used to draw the text.</param>
		/// <param name="text">The text to be drawn.</param>
		/// <param name="position">The position on the screen to draw the text.</param>
		/// <param name="color">The color of the text.</param>
		/// <param name="rotation">The rotation of the text in radians.</param>
		/// <param name="origin">The origin of the text rotation. Typically the center of the text.</param>
		/// <param name="scale">The scale of the text.</param>
		/// <param name="spriteEffects">Effects to apply to the text (e.g., flipping).</param>
		/// <param name="layerDepth">The depth of the layer on which the text is drawn.</param>
		public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            if (spriteFont == null) return;
            spriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

		/// <summary>
		/// Draws a texture on the screen.
		/// </summary>
		/// <param name="texture">The texture to be drawn.</param>
		/// <param name="position">The position on the screen to draw the texture.</param>
		/// <param name="sourceRectangle">The section of the texture to draw (null to draw the entire texture).</param>
		/// <param name="color">The color to tint the texture.</param>
		/// <param name="rotation">The rotation of the texture in radians.</param>
		/// <param name="origin">The origin of the texture rotation. Typically the center of the texture.</param>
		/// <param name="scale">The scale of the texture.</param>
		/// <param name="spriteEffects">Effects to apply to the texture (e.g., flipping).</param>
		/// <param name="layerDepth">The depth of the layer on which the texture is drawn.</param>
		public void DrawTexture(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
		{
			if (texture == null) return;
			spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
		}


	}
}
