using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utilities
{
	/// <summary>
	/// Represents a circle defined by a center point and radius.
	/// Provides functionality to check if a point is contained within the circle.
	/// </summary>
	public struct Circle
	{
		/// <summary>
		/// Gets or sets the center point of the circle.
		/// </summary>
		public Vector2 Center { get; set; }

		/// <summary>
		/// Gets or sets the radius of the circle.
		/// </summary>
		public float Radius { get; set; }

		/// <summary>
		/// Initializes a new instance of the Circle struct with a specified center and radius.
		/// </summary>
		/// <param name="center">The center point of the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
		public Circle(Vector2 center, float radius)
		{
			Center = center;
			Radius = radius; 
		}

		/// <summary>
		/// Determines whether the specified point is inside the circle.
		/// </summary>
		/// <param name="point">The point to check for containment.</param>
		/// <returns>true if the point is inside the circle; otherwise, false.</returns>
		public bool Contains(Vector2 point)
		{
			return Vector2.DistanceSquared(point, Center) <= Radius * Radius;
		}

	}
}
