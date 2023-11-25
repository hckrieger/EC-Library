using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.AssetManagers
{
    public class GraphicsAssetManager : IService
    {
        private ContentManager content;
        private GraphicsDevice graphics;

        private Dictionary<string, Texture2D> spriteCache = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fontCache = new Dictionary<string, SpriteFont>();
        private Dictionary<string, Texture2D> rectangleCache = new Dictionary<string, Texture2D>();
        public GraphicsAssetManager(ContentManager content, GraphicsDevice graphics)
        {
            this.content = content;
            this.graphics = graphics;

		}

       

        public void UnloadContent()
        {
            content.Unload();
        }

        public Texture2D LoadSprite(string spriteName)
        {
            if (!spriteCache.TryGetValue(spriteName, out Texture2D sprite))
            {
                sprite = content.Load<Texture2D>(spriteName);
                spriteCache[spriteName] = sprite;
            }
            return sprite;
        }

        public SpriteFont LoadFont(string fontName)
        {
            if (!fontCache.TryGetValue(fontName, out SpriteFont font))
            {
                font = content.Load<SpriteFont>(fontName);
                fontCache[fontName] = font;
            }
            return font;
        }

        public void ClearTotalCache()
        {
            spriteCache.Clear();
            fontCache.Clear();
            rectangleCache.Clear();
        }

        public Texture2D LoadRectangle(string rectangleName, int width, int height)
        {
            if (!rectangleCache.TryGetValue(rectangleName, out Texture2D rectangleTexture))
            {
				Color[] data = new Color[width * height];
				rectangleTexture = new Texture2D(graphics, width, height);
				for (int i = 0; i < data.Length; ++i)
					data[i] = Color.White;

				rectangleTexture.SetData(data);

                rectangleCache[rectangleName] = rectangleTexture;
			}


            return rectangleTexture;
		}
    }
}
