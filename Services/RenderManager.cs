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

        public void DrawSprite(Texture2D spriteTexture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
        {
            if (spriteTexture == null) return;
            spriteBatch.Draw(spriteTexture, position, sourceRectangle, color, rotation, origin, scale, spriteEffects, layerDepth);
        }

		public void DrawRectangle(Texture2D rectangleTexture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
		{
            if (rectangleTexture == null) return;
            spriteBatch.Draw(rectangleTexture, position, sourceRectangle ?? new Rectangle(0, 0, rectangleTexture.Width, rectangleTexture.Height), color, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }


    }
}
