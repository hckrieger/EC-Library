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
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace EC
{

	public class ExtendedGame : Game
	{


		protected GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private RenderTarget2D renderTarget;

		private Dictionary<Type, IService> commonServices;
		private InputManager inputManager;

		protected DisplayManager displayManager;

		protected bool IsFullScreen
		{
			set
			{
				displayManager.ToggleFullScreen(value);
			}
		}
	
		



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
			base.Initialize();
			displayManager = new DisplayManager(_graphics);

			SetWindowSize(1280, 720);



			UpdateRenderTarget();


			
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			

			ServiceRegistration();

			Components.Add(inputManager);

			
		}


		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!displayManager.IsInternalResolutionSetManually)
			{
				SetInternalResolution(displayManager.WindowSize.X, displayManager.WindowSize.Y);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			// Set the render target to our custom render target
			GraphicsDevice.SetRenderTarget(renderTarget);
			// Clear the render target with a default color
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin(sortMode: SpriteSortMode.FrontToBack, blendState: BlendState.AlphaBlend);
			// TODO: Add your drawing code here, which should use _spriteBatch to draw onto the renderTarget
			base.Draw(gameTime);
			_spriteBatch.End();

			// Reset the render target to draw everything we just did onto the screen
			GraphicsDevice.SetRenderTarget(null);
			GraphicsDevice.Clear(Color.Black); // Clear the back buffer




			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			
			_spriteBatch.Draw(renderTarget, displayManager.AdjustedViewport.Bounds, Color.White);
			_spriteBatch.End();
		}

		private void ServiceRegistration()
		{
			inputManager = new InputManager(displayManager, this);
			GraphicsAssetManager assetManager = new GraphicsAssetManager(Content, _graphics.GraphicsDevice);

			commonServices = new Dictionary<Type, IService>
			{
				{ typeof(RenderManager), new RenderManager(assetManager, _spriteBatch) },
				{ typeof(AudioAssetManager), new AudioAssetManager(Content) },
				{ typeof(DisplayManager), displayManager },
				{ typeof(InputManager), inputManager },
				{ typeof(CollisionManager), new CollisionManager() },
				{ typeof(SceneManager), new SceneManager(this) },
				
				//more services to be added
			};

			foreach (var kvp in commonServices)
				Services.AddService(kvp.Key, kvp.Value);
		}

		protected void SetWindowSize(int width, int height, bool applyChanges = true)
		{
			displayManager.WindowSize = new Point(width, height);
			if (applyChanges)
			{
				_graphics.PreferredBackBufferWidth = width;
				_graphics.PreferredBackBufferHeight = height;
				_graphics.ApplyChanges();
			}
		}



		protected void SetInternalResolution(int width, int height)
		{
			displayManager.InternalResolution = new Point(width, height);
			//displayManager.AdjustViewportForAspectRatio();
			UpdateRenderTarget();
		}

		private void UpdateRenderTarget()
		{
			if (renderTarget != null)
			{
				renderTarget.Dispose();
			}
			
			renderTarget = new RenderTarget2D(GraphicsDevice, displayManager.InternalResolution.X, displayManager.InternalResolution.Y);
		}
	}
}