using EC.Components;
using EC.Components.Render;
using EC.Components.Renderers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.CoreSystem
{
    public class Entity : DrawableGameComponent
    {
		/// <summary>
		/// Gets the unique identifier for the entity.
		/// </summary>
		public string ID { get; } = Guid.NewGuid().ToString();
        private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();

		private Renderer renderer;

		public Entity Parent => Transform.Parent?.Entity;

		/// <summary>
		/// Because the Transform component is frequently used I'm acessing it directly in the Entity
		/// </summary>
		public Transform Transform
		{
			get
			{
				if (!HasComponent<Transform>())
				{
					AddComponent(new Transform(this));
				}
				return GetComponent<Transform>();

			}


		}



		/// <summary>
		/// Initializes a new instance of the Entity class.
		/// </summary>
		/// <param name="game">The game instance to which this entity belongs.</param>
		public Entity(Game game) : base(game) 
        {

        }

		/// <summary>
		/// Adds a component to the entity.
		/// </summary>
		/// <param name="component">The component to add.</param>
		/// <exception cref="InvalidOperationException">Thrown when a component of the same type already ex
		public void AddComponent(Component component)
        {
            Type type = component.GetType();
            if (!components.ContainsKey(type))
            {
                components[type] = component;
                component.Initialize();
                component.Entity = this;
            }
            else
            {
                throw new InvalidOperationException($"Entity already has a component of type {type}");
            }

        }

		/// <summary>
		/// Adds multiple components to the entity.
		/// </summary>
		/// <param name="componentParams">The components to add.</param>
		/// <exception cref="InvalidOperationException">Thrown when a component of the same type already exists in the entity.</exception>
		public void AddComponents(params Component[] componentParams)
		{
            foreach (Component component in componentParams)
            {
				AddComponent(component);
            }


		}

		/// <summary>
		/// Retrieves a component of a specific type from the entity.
		/// </summary>
		/// <typeparam name="T">The type of component to retrieve.</typeparam>
		/// <returns>The component of the specified type, if it exists; otherwise, null.</returns>
		public T GetComponent<T>() where T : Component
        {
            Type type = typeof(T);
            if (components.TryGetValue(type, out Component component))
            {
                return component as T;
            }
            return null;
        }

		/// <summary>
		/// Checks whether the entity has a component of a specific type.
		/// </summary>
		/// <typeparam name="T">The type of component to check for.</typeparam>
		/// <returns>true if the entity has the component; otherwise, false.</returns>
		public bool HasComponent<T>() where T : Component
        {
            return GetComponent<T>() != null;
        }

		/// <summary>
		/// Removes a component of a specific type from the entity.
		/// </summary>
		/// <typeparam name="T">The type of component to remove.</typeparam>

		public void RemoveComponent<T>() where T : Component
        {
            Type type = typeof(T);
            if (components.ContainsKey(type))
            {
				components[type].DetachEvents();
                components.Remove(type);
            }
        }
		/// <summary>
		/// Removes all components from the entity.
		/// </summary>
		public void RemoveAllComponents()
		{
			foreach (var component in components.Values)
			{
				component.DetachEvents(); // Ensure this matches the method name defined in the Component class
			}
			components.Clear();
		}



		public override void Update(GameTime gameTime)
        {

			
				foreach (var kvp in components)
				{
					Component component = kvp.Value;
					if (component.IsEnabled)
					{
						component.Update(gameTime);
					}
				}
			


            base.Update(gameTime);
        }


		public override void Draw(GameTime gameTime)
		{
			
				foreach (var kvp in components)
				{
					Component component = kvp.Value;
					if (component.IsEnabled)
					{
						component.Draw();
					}
				}
			

			base.Draw(gameTime);
		}

		public virtual void Reset()
		{

		}

		//private bool IsParentRendererVisible()
		//{
		//	var rectangleRenderer = this.Parent?.GetComponent<RectangleRenderer>();
		//	var circleRenderer = this.Parent?.GetComponent<CircleRenderer>();

		//	// Check if either renderer is not null and visible
		//	return (rectangleRenderer != null && rectangleRenderer.IsVisible) ||
		//		   (circleRenderer != null && circleRenderer.IsVisible);
		//}
	}
}
