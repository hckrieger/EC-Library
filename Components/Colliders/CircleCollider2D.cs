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
	public class CircleCollider2D : Component
	{
		private Transform transform;
		private Origin origin;
		private Circle localBounds;
		private Circle boundsCache;
		private bool boundsNeedUpdate = true;

		//public Circle Bounds
		//{
		//	get
		//	{
		//		var position = Vector2.Zero;
		//		if  (Entity != null && Entity.HasComponent<Transform>())
		//		{
		//			position = Entity.GetComponent<Transform>().Position;
		//		}

		//		var originOffset = Vector2.Zero;
		//		if (Entity.HasComponent<Origin>())
		//		{
		//			Origin origin = Entity.GetComponent<Origin>();
		//			originOffset = origin.Value;
		//		}

		//		return new Circle(LocalBounds.Center + position - originOffset, LocalBounds.Radius);
		//	}
		//}

		public CircleCollider2D(float centerX, float centerY, float radius, Entity entity) : base(entity)
		{
			localBounds = new Circle(new Vector2(centerX, centerY), radius);

			transform = entity.GetComponent<Transform>();
			origin = entity.GetComponent<Origin>();

			if (transform != null) 
			{
				transform.PositionChanged += UpdateBoundsCache;
			}
		}

		private void UpdateBoundsCache()
		{
			boundsNeedUpdate = true;
		}

		public Circle LocalBounds
		{
			get { return localBounds; }
			set
			{
				localBounds = value;
				UpdateBoundsCache();
			}
		}

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

		private void UpdateGlobalBounds()
		{
			var position = transform?.Position ?? Vector2.Zero;
			var originOffset = origin?.Value ?? Vector2.Zero;

			boundsCache = new Circle(localBounds.Center + position - originOffset, localBounds.Radius);
		}

	
	}
}
