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
	/// Represents the base class for 2D colliders, providing common functionality and abstract definitions for collider-specific behavior.
	/// </summary>
	public abstract class Collider2D : Component
	{
		
		protected Transform transform;
		protected Origin origin;
		protected bool boundsNeedUpdate = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="Collider2D"/> class attached to the given entity.
		/// </summary>
		/// <param name="entity">The entity to which this collider is attached.</param>
		public Collider2D(Entity entity) : base(entity)
		{
			transform = entity.GetComponent<Transform>();
			origin = entity.GetComponent<Origin>();

			if (transform != null)
			{
				transform.PositionChanged += UpdateBoundsCache;
			}
		}

		/// <summary>
		/// Detaches all event handlers when the collider is destroyed or no longer in use.
		/// </summary>
		public override void DetachEvents()
		{
			if (transform != null)
			{
				transform.PositionChanged -= UpdateBoundsCache;
			}
		}

		/// <summary>
		/// Marks the bounds as needing an update. This should be called whenever the transform changes.
		/// </summary>
		protected virtual void UpdateBoundsCache()
		{
			boundsNeedUpdate = true;
		}

		/// <summary>
		/// Updates the global bounds of the collider based on the current transform and origin.
		/// </summary>
		protected abstract void UpdateGlobalBounds();

	}
}
