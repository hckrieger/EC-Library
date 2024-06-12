using EC.Components.Render;
using EC.CoreSystem;
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
	/// <summary>
	/// Manages the loading, caching, and unloading of graphical assets such as textures and fonts.
	/// This class uses caching to optimize the performance by reusing assets that are already loaded.
	/// </summary>
	public class GraphicsAssetManager : IService
    {
        private ContentManager content;
        private GraphicsDevice graphics;

		// Caches for textures and fonts.
		private Dictionary<string, Texture2D> textureCache = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> fontCache = new Dictionary<string, SpriteFont>();
        private Dictionary<string, SpriteSheet> spriteSheetCache = new Dictionary<string, SpriteSheet>();
		/// <summary>
		/// Initializes a new instance of the GraphicsAssetManager class.
		/// </summary>
		/// <param name="content">ContentManager used to load assets.</param>
		/// <param name="graphics">GraphicsDevice used for creating dynamic textures.</param>
		public GraphicsAssetManager(ContentManager content, GraphicsDevice graphics)
        {
            this.content = content;
            this.graphics = graphics;

		}


		/// <summary>
		/// Unloads all content managed by the ContentManager. This should be called when the content is no longer needed.
		/// </summary>
		public void UnloadTotalContent()
        {
            content.Unload();
            ClearTotalCache();
        }

		/// <summary>
		/// Loads a sprite texture from the content pipeline. If the sprite is already loaded,
		/// returns the cached version for performance optimization.
		/// </summary>
		/// <param name="spriteName">The name of the sprite to load.</param>
		/// <returns>The loaded Texture2D object.</returns>
		public Texture2D LoadSprite(string spriteName)
        {
            if (!textureCache.TryGetValue(spriteName, out Texture2D sprite))
            {
                sprite = content.Load<Texture2D>(spriteName);
                textureCache[spriteName] = sprite;
            }
            return sprite;
        }


		public void UnloadGraphicsAsset(string assetName)
		{
			if (textureCache.ContainsKey(assetName))
			{
				textureCache[assetName].Dispose();
				textureCache.Remove(assetName);
			}
			
			if (fontCache.ContainsKey(assetName)) 
			{
				fontCache.Remove(assetName);
			}
		}

	

		/// <summary>
		/// Loads a font from the content pipeline. If the font is already loaded,
		/// returns the cached version for performance optimization.
		/// </summary>
		/// <param name="fontName">The name of the font to load.</param>
		/// <returns>The loaded SpriteFont object.</returns>
		public SpriteFont LoadFont(string fontName)
        {
            if (!fontCache.TryGetValue(fontName, out SpriteFont font))
            {
                font = content.Load<SpriteFont>(fontName);
                fontCache[fontName] = font;
            }
            return font;
        }


		public SpriteSheet LoadSpriteSheet(string spriteSheetName, Entity entity, Point gridSize, int gridIndex)
		{
			if (!spriteSheetCache.TryGetValue(spriteSheetName, out SpriteSheet spriteSheet))
			{
				spriteSheet = new SpriteSheet(entity, gridSize);
				spriteSheetCache[spriteSheetName] = spriteSheet;
			} 
			spriteSheet.GridIndex = gridIndex;
			return spriteSheet;
		}

		/// <summary>
		/// Clears the entire cache, both textures and fonts. This can be useful when a large number of assets are no longer needed,
		/// such as when changing levels or scenes.
		/// </summary>
		public void ClearTotalCache()
        {
            textureCache.Clear();

			foreach(var texture in textureCache.Values)
				texture.Dispose();
			
            fontCache.Clear();
            spriteSheetCache.Clear();
        }

		/// <summary>
		/// Creates and loads a rectangle texture into the cache. If a rectangle texture with the same name already exists, 
		/// it returns the cached version.
		/// </summary>
		/// <param name="rectangleName">The unique name for the rectangle texture.</param>
		/// <param name="width">Width of the rectangle.</param>
		/// <param name="height">Height of the rectangle.</param>
		/// <returns>The created Texture2D object representing a rectangle.</returns>
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

		/// <summary>
		/// Creates and loads a circle texture into the cache. If a circle texture with the same name already exists,
		/// it returns the cached version.
		/// </summary>
		/// <param name="circleName">The unique name for the circle texture.</param>
		/// <param name="radius">Radius of the circle.</param>
		/// <returns>The created Texture2D object representing a circle.</returns>
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
