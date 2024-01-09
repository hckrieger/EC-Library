using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components
{
	/// <summary>
	/// Represents the origin point for an entity. 
	/// This class defines an offset value used for positioning or transformations relative to the entity.
	/// </summary>
	public class Origin : Component
	{
		private Vector2 offsetValue;

		/// <summary>
		/// Gets the offset value of the origin.
		/// </summary>
		public Vector2 Value
		{
			get
			{
				return offsetValue;
			}
		}

		/// <summary>
		/// Initializes a new instance of the Origin class, setting an offset value for the given entity.
		/// </summary>
		/// <param name="offsetValue">The offset value representing the origin point.</param>
		/// <param name="entity">The entity to which this origin component is attached.</param>
		public Origin(Vector2 offsetValue, Entity entity) : base(entity) 
		{
			this.offsetValue = offsetValue;
		}
	}
}
