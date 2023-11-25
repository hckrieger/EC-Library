using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components
{
	public class Origin : Component
	{
		private Vector2 offsetValue;

		//Was formerly this because I figure the spriteBatch would do the work of subtracting the origin
		//public Vector2 Value
		//{
		//	get
		//	{
		//		if (transform == null)
		//		{
		//			transform = Entity.GetComponent<Transform>();
		//		}
		//		return transform.Position + offsetValue;
		//	}
		//}

		public Vector2 Value
		{
			get
			{
				return offsetValue;
			}
		}

		public Origin(Vector2 offsetValue, Entity entity) : base(entity) 
		{
			this.offsetValue = offsetValue;
		}
	}
}
