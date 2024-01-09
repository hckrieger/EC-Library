using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.CoreSystem
{
	/// <summary>
	/// Base class for all components in the ECS framework. Provides basic functionalities 
	/// and properties common to all components.
	/// </summary>
	public class Component
    {
		/// <summary> The entity this component is attached to. </summary>
		public Entity Entity { get; set; }

		/// <summary> Indicates whether this component is enabled and should be updated. </summary>
		public bool IsEnabled { get; set; }

		/// <summary>
		/// Initializes a new instance of the Component class.
		/// </summary>
		/// <param name="entity">The entity to which this component is attached.</param>
		public Component(Entity entity)
        {
            Entity = entity; 
            IsEnabled = true;
        }

		/// <summary>
		/// Called when the component is destroyed or removed.
		/// </summary>
		public virtual void DetachEvents()
		{
			// Default implementation does nothing.
			// Override this in derived classes to handle event detachment.
		}



		public virtual void AttachEvents()
		{
			// Default implementation does nothing.
			// Override this in derived classes to handle event attachment.
		}

		/// <summary> Called when the component is first added to an entity. </summary>
		public virtual void Initialize()
        {

        }

		/// <summary> Called every frame to update the component. </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public virtual void Update(GameTime gameTime)
        {

        }

		/// <summary> Called every frame to draw the component, if applicable. </summary>
		public virtual void Draw()
        {

        }
    }
}
