using EC.CoreSystem;
using EC.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Colliders
{
	/// <summary>
	/// Represents a circular collider for 2D collision detection and physics interactions.
	/// </summary>
	public class CircleCollider2D : Collider2D
	{
		private Circle localBounds;
		private Circle boundsCache;

		/// <summary>
		/// Initializes a new instance of the CircleCollider2D class with specified dimensions and associated entity.
		/// </summary>
		/// <param name="centerX">The local X position of the collider's center.</param>
		/// <param name="centerY">The local Y position of the collider's center.</param>
		/// <param name="radius">The radius of the collider.</param>
		/// <param name="entity">The entity this collider is attached to.</param>
		public CircleCollider2D(Circle circle, Entity entity) : base(entity)
		{
			localBounds = circle;
		}



		/// <summary>
		/// Gets or sets the local bounds of the collider as a circle.
		/// </summary>
		public Circle LocalBounds
		{
			get { return localBounds; }
			set
			{
				localBounds = value;
				UpdateBoundsCache();
			}
		}

		/// <summary>
		/// Gets the global bounds of the collider, which accounts for the entity's position and origin.
		/// This property is lazily updated - it recalculates the bounds only if they are marked as needing an update.
		/// </summary>
		public Circle Bounds
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

		/// <summary>
		/// Calculates and updates the global bounds of the circular collider.
		/// This involves adjusting the local bounds by the current position and origin of the entity to which the collider is attached.
		/// </summary>
		protected override void UpdateGlobalBounds()
		{
			var position = transform?.Position ?? Vector2.Zero;
			var originOffset = origin?.Value ?? Vector2.Zero;

			boundsCache = new Circle(localBounds.Center + position - originOffset, localBounds.Radius);
		}

	
	}
}
