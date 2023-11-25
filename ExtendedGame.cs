using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using EC.Services;
using EC.Services.AssetManagers;
using Microsoft.Xna.Framework.Content;


namespace EC
{

	public class ExtendedGame : Game
	{


		protected GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		private Dictionary<Type, IService> commonServices;
		private InputManager inputManager;

		protected DisplayManager displayManager;

		protected int WindowWidth = 1280;
		protected int WindowHeight = 720;


		public SceneManager SceneManager
		{
			get { return Services.GetService<SceneManager>(); }
		}



		public ExtendedGame()
		{
			
			_graphics = new GraphicsDeviceManager(this);
			
			Content.RootDirectory = "Content";
		}



		protected override void Initialize()
		{
			displayManager = new DisplayManager();
			displayManager.Width = WindowWidth;
			displayManager.Height = WindowHeight;
			
			base.Initialize();
			
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			
			_graphics.PreferredBackBufferWidth = displayManager.Width;
			_graphics.PreferredBackBufferHeight = displayManager.Height;
			_graphics.ApplyChanges();
			ServiceRegistration();

			Components.Add(inputManager);
		}




		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			
			_spriteBatch?.Begin(sortMode: SpriteSortMode.FrontToBack);
			// TODO: Add your drawing code here
			base.Draw(gameTime);
			_spriteBatch?.End();
		}

		private void ServiceRegistration()
		{
			inputManager = new InputManager(displayManager, this);
			GraphicsAssetManager assetManager = new GraphicsAssetManager(Content, _graphics.GraphicsDevice);

			commonServices = new Dictionary<Type, IService>
			{
				{ typeof(SceneManager), new SceneManager(this) },
				{ typeof(DisplayManager), displayManager },
				{ typeof(InputManager), inputManager },
				{ typeof(RenderManager), new RenderManager(assetManager, _spriteBatch) },
				{ typeof(CollisionManager), new CollisionManager() },
				
				//more services to be added
			};

			foreach (var kvp in commonServices)
				Services.AddService(kvp.Key, kvp.Value);
		}


	}
}