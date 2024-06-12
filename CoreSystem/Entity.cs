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
    public class Entity //: DrawableGameComponent
    {
		/// <summary>
		/// Gets the unique identifier for the entity.
		/// </summary>
		public string ID { get; } = Guid.NewGuid().ToString();
        private readonly Dictionary<Type, Component> components = new Dictionary<Type, Component>();

		public Entity DefaultParent { get; set; } = null;

        public Entity Parent => Transform.Parent?.Entity;

		public bool Visible { get; set; } = true;
		public bool Enabled { get; set; } = true;

        public bool IntendedVisible { get; set; }
		public bool IntendedEnable { get; set; }



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

		//public void Active(bool trueOrFalse)
		//{
		//	Enabled = trueOrFalse;
		//	Visible = trueOrFalse;
		//}

		public bool IsActive
		{
			get => Visible && Enabled;

			set
			{
				Visible = value;
				Enabled = value;
			}
		}

		

		/// <summary>
		/// Initializes a new instance of the Entity class.
		/// </summary>
		/// <param name="game">The game instance to which this entity belongs.</param>
		public Entity(Game game) 
        {
			IntendedVisible = true;
			IntendedEnable = true;
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
			foreach (var component in components.Values)
			{
				if (component is T)
				{
					return (T)component;
				}
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


		public virtual void Initialize()
		{

		}


		public virtual void Update(GameTime gameTime)
        {

			
				foreach (var kvp in components)
				{
					Component component = kvp.Value;
					if (component.IsEnabled)
					{
						component.Update(gameTime);
					}
				}
			


        }


		public virtual void Draw(GameTime gameTime)
		{
			
				foreach (var kvp in components)
				{
					Component component = kvp.Value;
					if (component.IsEnabled)
					{
						component.Draw();
					}
				}
			

		}

		public virtual void Reset()
		{

		}


	}
}
