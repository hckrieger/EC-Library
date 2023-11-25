using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Utilities
{
	public static class MathUtils
	{
		private static readonly Random random = new Random();

		/// <summary>
		/// Calculate the velocity from the specified angle in degrees and speed (in units per frame).
		/// </summary>
		/// <param name="angleInDegrees">The angle in degrees.</param>
		/// <param name="speed">The speed (in units per frame).</param>
		/// <param name="unflipQuadrant">If true, convert angles for human readability.</param>
		/// <returns>The velocity vector.</returns>
		public static Vector2 VelocityFromDegrees(float angleInDegrees, float speed, bool unflipQuadrant = true)
		{
			if (unflipQuadrant)  
				angleInDegrees = 360 - angleInDegrees;

			var angleToRadians = MathHelper.ToRadians(angleInDegrees);
			return VelocityFromRadians(angleToRadians, speed);
		}

		/// <summary>
		/// Calculate the velocity from the specified angle in radians and speed (in units per frame).
		/// </summary>
		/// <param name="angleInRadians">The angle in radians.</param>
		/// <param name="speed">The speed (in units per frame).</param>
		/// <returns>The velocity vector.</returns>
		public static Vector2 VelocityFromRadians(float angleInRadians, float speed)
		{
			float speedX = MathF.Cos(angleInRadians) * speed;
			float speedY = MathF.Sin(angleInRadians) * speed;
			return new Vector2(speedX, speedY);
		}

		/// <summary>
		/// Calculate the normalized trajectory from the specified angle in degrees.
		/// </summary>
		/// <param name="angleInDegrees">The angle in degrees.</param>
		/// <param name="unflipQuadrant">If true, convert angles for human readability.</param>
		/// <returns>The normalized trajectory vector.</returns>
		public static Vector2 TrajectoryFromDegrees(float angleInDegrees, bool unflipQuadrant = true)
		{
			if (unflipQuadrant)  
				angleInDegrees = 360 - angleInDegrees;

			var angleToRadians = MathHelper.ToRadians(angleInDegrees);
			return TrajectoryFromRadians(angleToRadians);
		}

		/// <summary>
		/// Calculate the normalized trajectory from the specified angle in radians.
		/// </summary>
		/// <param name="angleInRadians">The angle in radians.</param>
		/// <returns>The normalized trajectory vector.</returns> 
		public static Vector2 TrajectoryFromRadians(float angleInRadians)
		{
			float speedX = MathF.Cos(angleInRadians);
			float speedY = MathF.Sin(angleInRadians);
			return new Vector2(speedX, speedY);
		}


		/// <summary>
		/// Generates a random number up to the maximum
		/// </summary>
		/// <param name="max">maximum number (exclusive) from zero</param>
		/// <returns>The random integer</returns> 
		public static int RandomInt(int max)
		{
			return random.Next(max);
		}

		/// <summary>
		/// Generates a random number between two integers.
		/// </summary>
		/// <param name="min">minimum number</param>
		/// <param name="max">maximum number (exclusive)</param>
		/// <returns>The random integer</returns> 
		public static int RandomInt(int min, int max)
		{
			return random.Next(min, max);
		}

		/// <summary>
		/// Generates a random double between two numbers
		/// </summary>
		/// <param name="min">Minimum number</param>
		/// <param name="max">Maximum number (exclusive)</param>
		/// <returns>The random double</returns> 
		public static double RandomDouble(double min, double max)
		{
			return (random.NextDouble() * (max - min)) + min;
		}

		/// <summary>
		/// Generates a random double up to a maximum number
		/// </summary>
		/// <param name="max">Maximum number (exclusive)</param>
		/// <returns>The random double</returns> 
		public static double RandomDouble(double max)
		{
			return random.NextDouble() * max;
		}

		/// <summary>
		/// Generates a random double between 0 and 1 (exclusive)
		/// </summary>
		/// <returns>The random double</returns> 
		public static double RandomDouble()
		{
			return random.NextDouble();
		}
	}
}
