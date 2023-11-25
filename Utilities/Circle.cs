using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utilities
{
	public struct Circle
	{
		public Vector2 Center { get; set; }
		public float Radius { get; set; }	

		public Circle(Vector2 center, float radius)
		{
			Center = center;
			Radius = radius; 
		}

		public bool Contains(Vector2 point)
		{
			return Vector2.DistanceSquared(point, Center) <= Radius * Radius;
		}

	}
}
