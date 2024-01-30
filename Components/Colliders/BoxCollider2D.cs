using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Colliders
{
	/// <summary>
	/// Represents a 2D axis-aligned bounding box collider component for collision detection and physics interactions.
	/// </summary>
	public class BoxCollider2D : Collider2D
	{
		private Rectangle localBounds;
		private Rectangle boundsCache;
		
		/// <summary>
		/// Initializes a new BoxCollider2D with specified dimensions.
		/// </summary>
		/// <param name="x">The local X position of the collider.</param>
		/// <param name="y">The local Y position of the collider.</param>
		/// <param name="width">The width of the collider.</param>
		/// <param name="height">The height of the collider.</param>
		/// <param name="entity">The entity this collider is attached to.</param>
		public BoxCollider2D(Rectangle rectangle, Entity entity) : base(entity)
		{

			localBounds = rectangle;
		}



		/// <summary>
		/// Gets or sets the local bounds of the collider.
		/// </summary>
		public Rectangle LocalBounds
		{
			get { return localBounds; }
			set
			{
				localBounds = value;
				UpdateBoundsCache();
			}
		}

		/// <summary>
		/// Gets the global bounds of the collider, accounting for the entity's position and origin.
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				if (boundsNeedUpdate)
				{
					UpdateGlobalBounds();
				    boundsNeedUpdate = false;
				}

				return boundsCache;
			}
		}

	//	public Vector2 CenterBounds => new Vector2(Bounds.X, Bounds.Y) + new Vector2(Bounds.C

		/// <summary>
		/// Calculates the global bounds of the collider.
		/// </summary>
		protected override void UpdateGlobalBounds()
		{
			var position = transform?.Position ??  Vector2.Zero;
			var originOffset = origin?.Value ?? Vector2.Zero;

			boundsCache = new Rectangle(
				(int)(localBounds.X + position.X - originOffset.X),
				(int)(localBounds.Y + position.Y - originOffset.Y),
				localBounds.Width,
				localBounds.Height);
		}




	}
}
