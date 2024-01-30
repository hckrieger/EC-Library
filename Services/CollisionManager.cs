using EC.Components.Colliders;
using EC.CoreSystem;
using EC.Utilities;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
	/// <summary>
	/// Manages collision detection and resolution for 2D shapes.
	/// </summary>
	public class CollisionManager : IService
	{
		/// <summary>
		/// Enumerates possible sides of a collision.
		/// </summary>
		public enum CollisionSide { Top, Bottom, Left, Right, None }


		public bool ShapesIntersect(BoxCollider2D shape1, BoxCollider2D shape2)
		{
			return shape1.Bounds.Intersects(shape2.Bounds);
		}


		public bool ShapesIntersect(CircleCollider2D shape1, CircleCollider2D shape2)
		{
			float radiusSum = shape1.Bounds.Radius + shape2.Bounds.Radius;
			return Vector2.DistanceSquared(shape1.Bounds.Center, shape2.Bounds.Center) <= radiusSum * radiusSum;
		}

		public bool ShapesIntersect(CircleCollider2D circle, BoxCollider2D box)
		{
			return CheckCircleBox(circle, box);
		}

		public bool ShapesIntersect(BoxCollider2D box, CircleCollider2D circle)
		{
			return CheckCircleBox(circle, box);
		}


		private bool CheckCircleBox(CircleCollider2D circle, BoxCollider2D box)
		{
			Vector2 closestPoint;
			closestPoint.X = MathHelper.Clamp(circle.Bounds.Center.X, box.Bounds.Left, box.Bounds.Right);
			closestPoint.Y = MathHelper.Clamp(circle.Bounds.Center.Y, box.Bounds.Top, box.Bounds.Bottom);

			float distanceSquared = Vector2.DistanceSquared(circle.Bounds.Center, closestPoint);
			float radiusSquared = circle.Bounds.Radius * circle.Bounds.Radius;

			return distanceSquared <= radiusSquared;

		}

		public bool ShapeOutOfBounds(CircleCollider2D circle, BoxCollider2D bounds)
		{
			Vector2 circleCenter = circle.Bounds.Center;
			float radius = circle.Bounds.Radius;

			if (circleCenter.X - radius < bounds.Bounds.Left ||
				circleCenter.X + radius > bounds.Bounds.Right ||
				circleCenter.Y - radius < bounds.Bounds.Top ||
				circleCenter.Y + radius > bounds.Bounds.Bottom)
			{
				return true;
			}

			return false;
		}

		public bool ShapeOutOfBounds(BoxCollider2D box, BoxCollider2D bounds)
		{
			if (box.Bounds.Left < bounds.Bounds.Left ||
				box.Bounds.Right > bounds.Bounds.Right ||
				box.Bounds.Top < bounds.Bounds.Top ||
				box.Bounds.Bottom > bounds.Bounds.Bottom)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Calculates the depth of intersection between two rectangles.
		/// </summary>
		/// <param name="rect1">The first rectangle collider.</param>
		/// <param name="rect2">The second rectangle collider.</param>
		/// <returns>A Rectangle representing the area of intersection. Returns a zero-size Rectangle if there is no intersection.</returns>
		public Rectangle IntersectionDepth(BoxCollider2D rect1, BoxCollider2D rect2)
		{
			if (!ShapesIntersect(rect1, rect2))
				return new Rectangle(0, 0, 0, 0);

			int xmin = Math.Max(rect1.Bounds.Left, rect2.Bounds.Left);
			int xmax = Math.Min(rect1.Bounds.Right, rect2.Bounds.Right);
			int ymin = Math.Max(rect1.Bounds.Top, rect2.Bounds.Top);
			int ymax = Math.Min(rect1.Bounds.Bottom, rect2.Bounds.Bottom);
			return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
		}



		/// <summary>
		/// Determines the side of the box collider that is colliding with the circle collider.
		/// </summary>
		/// <param name="box">The box collider.</param>
		/// <param name="circle">The circle collider.</param>
		/// <returns>The CollisionSide where the collision is occurring. Returns CollisionSide.None if there is no collision.</returns>
		public CollisionSide GetCollisionSide(BoxCollider2D box, CircleCollider2D circle)
		{
			float closestX = Math.Clamp(circle.Bounds.Center.X, box.Bounds.Left, box.Bounds.Right);
			float closestY = Math.Clamp(circle.Bounds.Center.Y, box.Bounds.Top, box.Bounds.Bottom);

			if (Vector2.DistanceSquared(new Vector2(closestX, closestY), circle.Bounds.Center) > circle.Bounds.Radius * circle.Bounds.Radius)
			{
				return CollisionSide.None;
			}

			Vector2 direction = circle.Bounds.Center - new Vector2(closestX, closestY);
			direction.Normalize();

			if (Math.Abs(direction.X) > Math.Abs(direction.Y))
			{
				return direction.X > 0 ? CollisionSide.Left : CollisionSide.Right;
			} else
			{
				return direction.Y > 0 ? CollisionSide.Top : CollisionSide.Bottom;
			}
		}
	}
}
