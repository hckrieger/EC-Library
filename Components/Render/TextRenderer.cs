using EC.Components.Renderers;
using EC.CoreSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Render
{
	public class TextRenderer : Renderer
	{
		public SpriteFont Font { get; set; }
		private string text;
		private Alignment textAlignment;
		private Vector2 alignedPosition;
		private Vector2 textSize;
		private Entity entity;
		private StringBuilder dynamicTextBuilder;
		private bool isDynamicText;

		/// <summary>
		/// Defines text alignment options.
		/// </summary>
		public enum Alignment
		{
			Left,
			Center,
			Right,
		}

		/// <summary>
		/// Gets or sets the text to be rendered.
		/// </summary>
		public string Text
		{
			get => text;
			set
			{
				if (isDynamicText)
					throw new InvalidOperationException("Cannot set both Text and DynamicText. Please clear DynamicText before setting Text.");

                text = value;
				isDynamicText = false;
				textSize = Font.MeasureString(text);
				CalculateAlignedPosition();
			}
		}

		public string DynamicText
		{
			get => dynamicTextBuilder?.ToString();
			set
			{
				if (!string.IsNullOrEmpty(text))
					throw new InvalidOperationException("Cannot set both Text and DynamicText. Please clear Text before setting DynamicText.");

				if (dynamicTextBuilder == null)
					dynamicTextBuilder = new StringBuilder();

				dynamicTextBuilder.Clear();
				dynamicTextBuilder.Append(value);
				isDynamicText = true;
				textSize = Font.MeasureString(dynamicTextBuilder.ToString());
				CalculateAlignedPosition();
			}
		}

		/// <summary>
		/// Gets or sets the alignment of the text.
		/// </summary>
		public Alignment TextAlignment
		{
			get => textAlignment;
			set
			{
				textAlignment = value;
				CalculateAlignedPosition();
			}
		}

		/// <summary>
		/// Gets the size of the text
		/// </summary>
		public Vector2 GetSize
		{
			get => textSize;
		}

		/// <summary>
		/// Gets the width of text in pixels
		/// </summary>
		public float Width => textSize.X;

		/// <summary>
		/// Gets the height of text in pixels
		/// </summary>
		public float Height => textSize.Y;


        public string FontName { get; set; }

        /// <summary>
        /// Initializes a new instance of the TextRenderer class.
        /// </summary>
        /// <param name="fontName">The name of the Font to use for rendering the text.</param>
        /// <param name="text">The initial text to render.</param>
        /// <param name="color">The color of the text. Inherited from the Renderer class.</param>
        /// <param name="game">The game instance this renderer belongs to.</param>
        /// <param name="entity">The entity this renderer is associated with.</param>
        public TextRenderer(string fontName, string initialText, Color color, Game game, Entity entity)
			: base(game, entity)
		{
			Font = renderManager.GraphicsAssetManager.LoadFont(fontName);
			FontName = fontName;
			this.entity = entity;
			
			
			Color = color; // Inherits Color property from Renderer
			textAlignment = Alignment.Left; // Default alignment
			isDynamicText = false;
			textSize = Font.MeasureString(initialText);
			text = initialText;
			CalculateAlignedPosition();
		}

		private void CalculateAlignedPosition()
		{
			if (Font == null) return;

			
			alignedPosition = Transform?.Position ?? Vector2.Zero;

			if (textAlignment == Alignment.Left)
			{
				if (entity.HasComponent<Origin>())
					entity.RemoveComponent<Origin>();
				return;
			} else if (!entity.HasComponent<Origin>())
			{
				entity.AddComponent(new Origin(Vector2.Zero, entity));
			}

			var origin = entity.GetComponent<Origin>();	

			switch (textAlignment)
			{
						

				case Alignment.Center:
					origin.Value = textSize / 2;
					break;
				case Alignment.Right:
					origin.Value = new Vector2(textSize.X, 0);
					break;
				case Alignment.Left:
					origin.Value = Vector2.Zero;
					break;
			}
		}

		/// <summary>
		/// Draws the text using the provided SpriteFont.
		/// Inherits the Draw method from Renderer and overrides it for text rendering.
		/// </summary>
		public override void Draw()
		{
			if (IsEntityVisible() && Font != null) // Inherits IsVisible property from Renderer
			{
				string textToDraw = isDynamicText ? dynamicTextBuilder.ToString() : text;
				renderManager.DrawString(Font, textToDraw, Transform.Position, Color, Transform?.Rotation ?? 0, Origin?.Value ?? Vector2.Zero, Transform?.Scale ?? 1f, SpriteEffects, LayerDepth);
			}
		}
	}
}
