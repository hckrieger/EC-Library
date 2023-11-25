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
	public class CollisionManager : IService
	{

		public enum CollisionSide { Top, Bottom, Left, Right, None }

		//Two circles colliding
		public bool ShapesIntersect(CircleCollider2D circle1, CircleCollider2D circle2)
		{
			float radiusSum = circle1.Bounds.Radius + circle2.Bounds.Radius;
			return Vector2.DistanceSquared(circle1.Bounds.Center, circle2.Bounds.Center) <=  radiusSum * radiusSum;
		}


		//Two rectangles colliding
		public bool ShapesIntersect(BoxCollider2D box1, BoxCollider2D box2)
		{
			return box1.Bounds.Intersects(box2.Bounds);
		}



		//Rectangle and Circle colliding
		public bool ShapesIntersect(BoxCollider2D box, CircleCollider2D circle)
		{
	
			return CircleAndRectangleIntersectionLogic(circle, box);

		}

		//Rectangle and Circle colliding but with the paramenters in a different order
		public bool ShapesIntersect(CircleCollider2D circle, BoxCollider2D box)
		{
			return CircleAndRectangleIntersectionLogic(circle, box);
		}

		private bool CircleAndRectangleIntersectionLogic(CircleCollider2D circle, BoxCollider2D box) 
		{
			Vector2 closestPoint;


			closestPoint.X = MathHelper.Clamp(circle.Bounds.Center.X, box.Bounds.Left, box.Bounds.Right);
			closestPoint.Y = MathHelper.Clamp(circle.Bounds.Center.Y, box.Bounds.Top, box.Bounds.Bottom);

			float distanceSquared = Vector2.DistanceSquared(circle.Bounds.Center, closestPoint);
			return distanceSquared <= circle.Bounds.Radius * circle.Bounds.Radius;
		}

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
