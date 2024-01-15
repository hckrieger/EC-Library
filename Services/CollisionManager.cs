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


		/// <summary>
		/// Determines if a circle and a rectangle intersect.  
		/// </summary>
		/// <param name="circle">Circle collider</param>
		/// <param name="box">Box collider</param>
		/// <returns></returns>
		private bool CheckCircleBoxIntersection(CircleCollider2D circle, BoxCollider2D box) 
		{
			Vector2 closestPoint;


			closestPoint.X = MathHelper.Clamp(circle.Bounds.Center.X, box.Bounds.Left, box.Bounds.Right);
			closestPoint.Y = MathHelper.Clamp(circle.Bounds.Center.Y, box.Bounds.Top, box.Bounds.Bottom);

			float distanceSquared = Vector2.DistanceSquared(circle.Bounds.Center, closestPoint);
			return distanceSquared <= circle.Bounds.Radius * circle.Bounds.Radius;
		}


		/// <summary>ef
		/// Determines if two colliders intersect. Supports BoxBox, CircleCircle, and BoxCircle collisions.
		/// </summary>
		/// <param name="collider1">The first collider, which can be either a BoxCollider2D or a CircleCollider2D.</param>
		/// <param name="collider2">The second collider, which can be either a BoxCollider2D or a CircleCollider2D.</param>
		/// <returns>True if the colliders intersect; otherwise, false.</returns>
		/// <remarks>
		/// This method checks for intersections between two colliders, handling different combinations:
		/// - Box-Box collision using the Intersects method of Rectangle.
		/// - Circle-Circle collision using the distance between centers and the sum of radii.
		/// - Box-Circle and Circle-Box collision using the closest point to the circle on the box and checking the distance to the circle's center.
		/// This method uses type checking and casting to determine the types of the colliders and then calls the appropriate logic for each type of collision.
		/// </remarks>
		public bool ShapesIntersect(Collider2D collider1, Collider2D collider2)
		{
			if (collider1 is BoxCollider2D box1 && collider2 is BoxCollider2D box2)
			{
				return box1.Bounds.Intersects(box2.Bounds);
			}
			else if (collider1 is CircleCollider2D circle1 && collider2 is CircleCollider2D circle2)
			{
				float radiusSum = circle1.Bounds.Radius + circle2.Bounds.Radius;
				return Vector2.DistanceSquared(circle1.Bounds.Center, circle2.Bounds.Center) <= radiusSum * radiusSum;
			}
			else if (collider1 is BoxCollider2D box && collider2 is CircleCollider2D circle)
			{
				return CheckCircleBoxIntersection(circle, box);
			}
			else if (collider1 is CircleCollider2D circleX && collider2 is BoxCollider2D boxX)
			{
				return CheckCircleBoxIntersection(circleX, boxX);
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
