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
    public class RenderManager : IService
    {
		public GraphicsAssetManager GraphicsAssetManager { get; private set; }
  
        private SpriteBatch spriteBatch;


        public RenderManager(GraphicsAssetManager graphicsAssetManager, SpriteBatch spriteBatch)
        {
            GraphicsAssetManager = graphicsAssetManager;
            this.spriteBatch = spriteBatch;
            
        
        }


        public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            if (spriteFont == null) return;
            spriteBatch.DrawString(spriteFont, text, position, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

		public void DrawTexture(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
		{
			if (texture == null) return;
			spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
		}


	}
}
