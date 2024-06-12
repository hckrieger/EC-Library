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
	/// <summary>
	/// Manages display settings for the game, including handling resolution, fullscreen mode, and viewport adjustments.
	/// Integrates with <see cref="GraphicsDeviceManager"/> to apply graphics settings.
	/// </summary>
	public class DisplayManager : IService
	{
		private GraphicsDeviceManager graphics;
		private bool isFullScreen;

		/// <summary>
		/// Gets or sets a value indicating whether the internal resolution is set manually.
		/// When set to true, the viewport adjusts according to the manually set internal resolution.
		/// </summary>
		public bool IsInternalResolutionSetManually { get; set; } = false;

		private Point internalResolution;
		private Point windowSize;

        public Viewport AdjustedViewport { get; set; }

        public int WindowWidth => WindowSize.X;
		public int WindowHeight => WindowSize.Y;

		public Vector2 WindowCenter => new Vector2(windowSize.X, windowSize.Y);
		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayManager"/> class.
		/// </summary>
		/// <param name="graphics">The GraphicsDeviceManager instance used for managing display settings.</param>

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

		/// <summary>
		/// Adjusts the window size and the back buffer to reflect the current settings. 
		/// This method is only called when the game is in windowed mode.
		/// </summary>

		private void AdjustWindowSizeAndBackBuffer()
		{
			if (!isFullScreen)
			{
				graphics.PreferredBackBufferWidth = windowSize.X;
				graphics.PreferredBackBufferHeight = windowSize.Y;
				graphics.ApplyChanges();
			}
		}

		/// <summary>
		/// Applies initial display settings including fullscreen mode, internal resolution, and window size.
		/// </summary>
		/// <param name="fullScreen">Whether the display should start in fullscreen mode.</param>
		/// <param name="internalResolution">The desired internal resolution for the game.</param>
		/// <param name="windowSize">The initial size of the game window.</param>
		public void ApplyInitialSettings(bool fullScreen, Point internalResolution, Point windowSize)
		{
			this.InternalResolution = internalResolution;
			this.WindowSize = windowSize;
			ToggleFullScreen(fullScreen);
		}

		/// <summary>
		/// Toggles the game's fullscreen state. Adjusts the back buffer size to match the screen's resolution in fullscreen mode,
		/// or applies the window size settings when in windowed mode.
		/// </summary>
		/// <param name="fullScreen">If set to <c>true</c>, the game switches to fullscreen mode. If <c>false</c>, the game switches to windowed mode.</param>

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



		/// <summary>
		/// Calculates and applies a viewport that maintains the aspect ratio of the internal resolution,
		/// centered within the current window size or fullscreen resolution. This method ensures that the game's
		/// graphics are not stretched or squashed, maintaining the 
		/// aspect ratio.
		/// </summary>

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

		/// <summary>
		/// Converts screen coordinates to viewport coordinates, allowing for interactions to be accurately
		/// mapped in a scaled or letterboxed viewport.
		/// </summary>
		/// <param name="windowCoordinates">The coordinates on the window to be converted.</param>
		/// <returns>The corresponding coordinates within the viewport, adjusted for any scaling or letterboxing.</returns>

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
