using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components.Colliders
{
	public class BoxCollider2D : Component
	{
		private Transform transform;
		private Origin origin;
		private Rectangle localBounds;
		private Rectangle boundsCache;
		private bool boundsNeedsUpdate = true;


		public BoxCollider2D(int x, int y, int width, int height, Entity entity) : base(entity)
		{

			localBounds = new Rectangle(x, y, width, height);

			transform = entity.GetComponent<Transform>();
			origin = entity.GetComponent<Origin>();

			if (transform != null)
			{
				transform.PositionChanged += UpdateBoundsCache;
			}
		}

		private void UpdateBoundsCache() 
		{
			boundsNeedsUpdate = true;
		}

		public Rectangle LocalBounds
		{
			get { return localBounds; }
			set
			{
				localBounds = value;
				UpdateBoundsCache();
			}
		}

		public Rectangle Bounds
		{
			get
			{
				if (boundsNeedsUpdate)
				{
					UpdateGlobalBounds();
					boundsNeedsUpdate = false;
				}

				return boundsCache;
			}
		}

		private void UpdateGlobalBounds()
		{
			var position = transform?.Position ?? Vector2.Zero;
			var originOffset = origin?.Value ?? Vector2.Zero;

			boundsCache = new Rectangle(
				(int)(localBounds.X + position.X - originOffset.X),
				(int)(localBounds.Y + position.Y - originOffset.Y),
				localBounds.Width,
				localBounds.Height);
		}

		~BoxCollider2D()
		{
			if (transform != null)
			{
				transform.PositionChanged -= UpdateBoundsCache;
			}
		}
	}
}
