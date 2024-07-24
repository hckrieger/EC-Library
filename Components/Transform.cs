using EC.Components.Render;
using EC.Components.Renderers;
using EC.CoreSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components
{
	/// <summary>
	/// Represents the position, rotation, and scale of an entity, both locally and in the world context.
	/// It allows for hierarchical transformations by maintaining parent-child relationships.
	/// </summary>
	public class Transform : Component
	{
		private Vector2 position;
		private float rotation;
		private float scale = 1f;
		private Transform parent;
		private List<Transform> children = null;
		private Entity entity;


		public Entity Entity => entity;

		/// <summary> Triggered when the position is changed. </summary>
		public event Action PositionChanged;

		/// <summary> Gets or sets the local position of the entity. </summary>
		public Vector2 LocalPosition
		{
			get { return position; } 
			set 
			{ 
				if (position != value)
				{
					position = value;
					PositionChanged?.Invoke();
				}
			}
		}

		/// <summary> Gets or sets the local rotation of the entity. </summary>
		public float LocalRotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

		/// <summary> Gets or sets the local scale of the entity. </summary>
		public float LocalScale
		{
			get { return scale; }
			set { scale = value; }
		}

		/// <summary>
		/// Gets or sets the parent of this transform. Changing the parent will update the world transformations accordingly.
		/// </summary>
		public Transform Parent
		{
			get { return parent; }
			set
			{
				//If the new parent being assigned is different than it's current parent
				if (parent != value)
				{
				
					if (parent != null)
					{
						//If you remove the parent then keep the same world coordinates.
						//or if you switch parents, keep the world coordinates the same unless explicitly changed, otherwise. 
						LocalPosition = Position - entity.DefaultParent.Transform.Position;
						LocalRotation = Rotation;
						LocalScale = Scale; 
					}


					//remove childship of former parent entity
					if (parent != null && parent.children != null)
						parent.children.Remove(this);

					//set new value as parent, either null or another entity
					parent = value;

					if (parent != null)
					{
						//if it's another entity that doesn't have children then create a list for it to maintain children and add this entity object
						if (parent.children == null)
							parent.children = new List<Transform>();
						parent.children.Add(this);

					} else
					{
						parent = entity.DefaultParent.Transform;
					}


				}

			}
		}


		/// <summary> Gets the world position of the entity, considering the hierarchy. </summary>
		public Vector2 Position
		{
			get
			{
				return parent != null ? parent.Position + position : position; 
			}
		}

		/// <summary> Gets the world rotation of the entity, considering the hierarchy. </summary>
		public float Rotation
		{
			get
			{
				return parent != null ? parent.Rotation + rotation : rotation;
			}
		}


		/// <summary> Gets the world scale of the entity, considering the hierarchy. </summary>
		public float Scale
		{
			get
			{
				return parent != null ? parent.Scale * scale : scale;
			}
		}

		/// <summary>
		/// Initializes a new instance of the Transform class.
		/// </summary>
		/// <param name="entity">The entity to which this transform is attached.</param>
		public Transform(Entity entity) : base(entity)
		{
			position = Vector2.Zero;
			rotation = 0;
			this.entity = entity;
		}

	}
}
