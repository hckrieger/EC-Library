using EC.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
	public class DisplayManager : IService
	{
		private GraphicsDeviceManager graphics;
		private bool isFullScreen;
		public bool IsInternalResolutionSetManually { get; set; } = false;

		private Point internalResolution;
		private Point windowSize;

        public Viewport AdjustedViewport { get; set; }

        public int WindowWidth => WindowSize.X;
		public int WindowHeight => WindowSize.Y;

		public Vector2 WindowCenter => new Vector2(windowSize.X, windowSize.Y);

        public DisplayManager(GraphicsDeviceManager graphics)
        {
			this.graphics = graphics;
			this.isFullScreen = graphics.IsFullScreen;
			windowSize = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

			//internalResolution = this.WindowSize;
        }

		public Point InternalResolution
		{
			get => internalResolution;
			set
			{
				internalResolution = value;
				IsInternalResolutionSetManually = true;
				AdjustViewportForAspectRatio();
			}
		}

		public Point WindowSize
		{
			get => windowSize;
			set
			{
				windowSize = value;

				if (!IsInternalResolutionSetManually)
				{
					internalResolution = value;
				}

				AdjustWindowSizeAndBackBuffer();
				AdjustViewportForAspectRatio();
			}
		}

		private void AdjustWindowSizeAndBackBuffer()
		{
			if (!isFullScreen)
			{
				graphics.PreferredBackBufferWidth = windowSize.X;
				graphics.PreferredBackBufferHeight = windowSize.Y;
				graphics.ApplyChanges();
			}
		}

		public void ApplyInitialSettings(bool fullScreen, Point internalResolution, Point windowSize)
		{
			this.InternalResolution = internalResolution;
			this.WindowSize = windowSize;
			ToggleFullScreen(fullScreen);
		}

		public void ToggleFullScreen(bool fullScreen) 
		{
			isFullScreen = fullScreen;
			graphics.IsFullScreen = isFullScreen;

			if (isFullScreen)
			{
				graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
			else
			{
				AdjustWindowSizeAndBackBuffer();
			}

			graphics.ApplyChanges();
			AdjustViewportForAspectRatio();
		}

	

		
		public void AdjustViewportForAspectRatio()
		{

			if (internalResolution.X == 0 || internalResolution.Y == 0) return;

			float targetAspectRatio = (float)InternalResolution.X / InternalResolution.Y;
			int viewportWidth, viewportHeight;
			float windowAspectRatio = (float)graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight;

			if (windowAspectRatio > targetAspectRatio)
			{
				viewportHeight = graphics.PreferredBackBufferHeight;
				viewportWidth = (int)(viewportHeight * targetAspectRatio);
			}
			else
			{
				viewportWidth = graphics.PreferredBackBufferWidth;
				viewportHeight = (int)(viewportWidth / targetAspectRatio);
			}

			int viewportX = (graphics.PreferredBackBufferWidth - viewportWidth) / 2;
			int viewportY = (graphics.PreferredBackBufferHeight - viewportHeight) / 2;

			AdjustedViewport = new Viewport(viewportX, viewportY, viewportWidth, viewportHeight);
			graphics.GraphicsDevice.Viewport = AdjustedViewport;
		}

		public Vector2 ConvertWindowToViewport(Vector2 windowCoordinates)
		{
			var viewport = graphics.GraphicsDevice.Viewport;

			float scaleX = (float)viewport.Width / InternalResolution.X;
			float scaleY = (float)viewport.Height / InternalResolution.Y;

			float offsetX = viewport.X;
			float offsetY = viewport.Y;

			float adjustedX = (windowCoordinates.X - offsetX) / scaleX;
			float adjustedY = (windowCoordinates.Y - offsetY) / scaleY;

			return new Vector2(adjustedX, adjustedY);
		}

	


	}
}
