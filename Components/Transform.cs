using EC.CoreSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace EC.Components
{
	public class Transform : Component
	{
		private Vector2 position;
		private float rotation;
		private float scale = 1f;
		private Transform parent;
		private List<Transform> children = null;

		public event Action PositionChanged;

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

		public float LocalRotation
		{
			get { return rotation; }
			set { rotation = value; }
		}

		public float LocalScale
		{
			get { return scale; }
			set { scale = value; }
		}

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
						LocalPosition = Position;
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

					}
				}

			}
		}
		//WorldPosition
		public Vector2 Position
		{
			get
			{
				return parent != null ? parent.Position + position : position; 
			}
		}

		//WorldRotation
		public float Rotation
		{
			get
			{
				return parent != null ? parent.Rotation + rotation : rotation;
			}
		}


		//WorldScale
		public float Scale
		{
			get
			{
				return parent != null ? parent.Scale * scale : scale;
			}
		}

		public Transform(Entity entity) : base(entity)
		{
			position = Vector2.Zero;
			rotation = 0;
		}

	}
}
