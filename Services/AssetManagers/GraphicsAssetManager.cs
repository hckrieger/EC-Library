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

        private Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fontCache = new Dictionary<string, SpriteFont>();
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
            if (!textureCache.TryGetValue(spriteName, out Texture2D sprite))
            {
                sprite = content.Load<Texture2D>(spriteName);
                textureCache[spriteName] = sprite;
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
            textureCache.Clear();
            fontCache.Clear();
        }

        public Texture2D LoadRectangle(string rectangleName, int width, int height)
        {
            if (!textureCache.TryGetValue(rectangleName, out Texture2D rectangleTexture))
            {
				Color[] data = new Color[width * height];
				rectangleTexture = new Texture2D(graphics, width, height);
				for (int i = 0; i < data.Length; ++i)
					data[i] = Color.White;

				rectangleTexture.SetData(data);

                textureCache[rectangleName] = rectangleTexture;
			}


            return rectangleTexture;
		}

        public Texture2D LoadCircle(string circleName, int radius)
        {
           if (!textureCache.TryGetValue(circleName, out Texture2D circleTexture))
           {
                int diameter = radius * 2;
                circleTexture = new Texture2D(graphics, diameter, diameter);
                Color[] data = new Color[diameter * diameter];

                //Calculate the radius squared for efficiency
                int radiusSquared = radius * radius;
                int centerX = radius;
                int centerY = radius;

                for (int x = 0; x < diameter; x++)
                {
					for (int y = 0; y < diameter; y++)
					{
                        int dx = x - centerX;
                        int dy = y - centerY;
                        int distanceSquared = dx * dx + dy * dy;    

                        // If the pixel is within the circle's radius, set it to white.
                        if (distanceSquared <= radiusSquared)
                        {
                            data[x + y * diameter] = Color.White;
                        }
                        else
                        {
                            data[x + y * diameter] = Color.Transparent;
                        }
					}
				}

                circleTexture.SetData(data);
                textureCache[circleName] = circleTexture;

           }

            return circleTexture;
        }
    }
}
