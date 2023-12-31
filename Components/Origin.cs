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
