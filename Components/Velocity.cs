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
	/// Component representing the velocity of an entity. Modifies the entity's position over time.
	/// </summary>
	public class Velocity : Component
	{

		/// <summary> The velocity value applied to the entity's transform. </summary>
		public Vector2 Value { get; set; }
		private Transform transform;

		/// <summary>
		/// Initializes a new instance of the Velocity component.
		/// </summary>
		/// <param name="transform">The transform component of the entity to which this component is attached.</param>
		/// <param name="entity">The entity to which this component is attached.</param>
		public Velocity(Transform transform, Entity entity) : base(entity)
		{
			this.transform = transform;
		}

		/// <summary>
		/// Updates the entity's position based on its velocity.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			transform.LocalPosition += Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
	
}
