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
		private GraphicsAssetManager assetManager;
        private SpriteBatch spriteBatch;


        public RenderManager(GraphicsAssetManager assetManager, SpriteBatch spriteBatch)
        {
            this.assetManager = assetManager;
            this.spriteBatch = spriteBatch;
            
        
        }




        public void DrawString(string fontName, string text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(assetManager.LoadFont(fontName), text, position, color);
        }

        public void DrawSprite(string spriteName, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale)
        {
            
            spriteBatch.Draw(assetManager.LoadSprite(spriteName), position, sourceRectangle, color, rotation, origin, scale, SpriteEffects.None, 1f);
        }

        public void DrawRectangle(string rectangleName, Vector2 position, int width, int height, Color color, Vector2 origin, float rotation = 0f, float scale = 1f, float layerDepth = 1f)
        {
            spriteBatch.Draw(assetManager.LoadRectangle(rectangleName, width, height), position, null, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }
    }
}
