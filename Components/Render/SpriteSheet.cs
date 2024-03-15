using EC.Components.Colliders;
using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Render
{
	/// <summary>
	/// Represents a sprite sheet component that allows for easy management and usage of sprite sheets.
	/// Requires an entity to have a SpriteRenderer component before being added.
	/// </summary>
	public class SpriteSheet 
	{
		private SpriteRenderer spriteRenderer;
		private Point gridSize;
		private int gridIndex;
		public Rectangle? SourceRectangle { get; set; }



		/// <summary>
		/// Gets the width of the texture associated with the sprite renderer.
		/// </summary>
		public int TextureWidth => spriteRenderer.TextureWidth;

		/// <summary>
		/// Gets the height of the texture associated with the sprite renderer.
		/// </summary>
		public int TextureHeight => spriteRenderer.TextureHeight;


		/// <summary>
		/// Gets the number of columns in the sprite sheet grid.
		/// </summary>
		public int Columns => TextureWidth / GridWidth;

		/// <summary>
		/// Gets the number of rows in the sprite sheet grid.
		/// </summary>
		public int Rows => TextureHeight / GridHeight;


		/// <summary>
		/// Gets the width of each grid cell in the sprite sheet.
		/// </summary>
		public int GridWidth => gridSize.X;

		/// <summary>
		/// Gets the height of each grid cell in the sprite sheet.
		/// </summary>
		public int GridHeight => gridSize.Y;


		/// <summary>
		/// Gets the total number of grids (cells) in the sprite sheet.
		/// </summary>
		private int NumberOfGrids => Columns * Rows;

		/// <summary>
		/// Gets or sets the current grid index to display from the sprite sheet. Throws ArgumentOutOfRangeException if set to an invalid index.
		/// </summary>
		public int GridIndex
		{
			get => gridIndex;
			set
			{
				if (value >= 0 && value < NumberOfGrids)
				{
					gridIndex = value;
					UpdateSourceRectangle();
				}
				else
				{
					throw new ArgumentOutOfRangeException(nameof(gridIndex), "The provided grid index is out of range.");
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the SpriteSheet component with specified grid dimensions and initial grid index.
		/// </summary>
		/// <param name="gridDimension">The size of each grid of the sprite sheet in pixels</param>
		/// <param name="gridIndex">The initial index of the grid to display.</param>
		/// <exception cref="InvalidOperationException">Thrown if the entity does not have a SpriteRenderer component.</exception>
		public SpriteSheet(Entity entity, Point gridSize) 
		{
			this.spriteRenderer = entity.GetComponent<SpriteRenderer>();
			if (this.spriteRenderer == null)
			{
				throw new InvalidOperationException("SpriteSheet requires an entity with a SpriteRenderer component.");
			}

			this.gridSize = gridSize;
			UpdateSourceRectangle();

			//if (entity.HasComponent<BoxCollider2D>())
			//{
			//	entity.GetComponent<BoxCollider2D>().LocalBounds = new Rectangle(0, 0, GridWidth, GridHeight);
			//}
		}

		/// <summary>
		/// Updates the source rectangle based on the current grid index.
		/// </summary>
		private void UpdateSourceRectangle()
		{
			int columnIndex = gridIndex % Columns;
			int rowIndex = gridIndex / Columns;
			SourceRectangle = new Rectangle(columnIndex * GridWidth, rowIndex * GridHeight, GridWidth, GridHeight);
		}


	}
}
