using EC.CoreSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components
{
	public class Velocity : Component
	{
		public Vector2 Value { get; set; }
		private Transform transform;

		public Velocity(Transform transform, Entity entity) : base(entity)
		{
			this.transform = transform;
		}

		public override void Update(GameTime gameTime)
		{
			transform.LocalPosition += Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
	
}
