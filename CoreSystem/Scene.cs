using EC.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.CoreSystem
{
	/// <summary>
	/// Represents a scene in the game, which is a container for all entities present in a particular game state or level.
	/// This class handles adding, removing, and managing the state of these entities.
	/// </summary>
	public class Scene : DrawableGameComponent
	{
		protected List<Entity> entities;
		private List<Entity> entitiesToAdd;
		private List<Entity> entitiesToRemove;

		public string ID;


		protected Entity RootEntity { get; }

		/// <summary>
		/// Initializes a new instance of the Scene class.
		/// </summary>
		/// <param name="game">The game instance to which this scene belongs.</param>
		public Scene(Game game) : base(game)
		{
			entities = new List<Entity>();
			entitiesToAdd = new List<Entity>();
			entitiesToRemove = new List<Entity>();

			RootEntity = new Entity(game);
			RootEntity.AddComponent(new Transform(RootEntity));
			AddEntity(RootEntity);
		}

		

		/// <summary>
		/// Adds a single entity to the scene. The entity will be added at the beginning of the next update cycle.
		/// </summary>
		/// <param name="entity">The entity to add to the scene.</param>
		protected void AddEntity(Entity entity)
		{
			if (!entitiesToAdd.Contains(entity) && !entities.Contains(entity)) 
				entitiesToAdd.Add(entity);
		}

		/// <summary>
		/// Removes a single entity from the scene. The entity will be removed at the beginning of the next update cycle.
		/// </summary>
		/// <param name="entity">The entity to remove from the scene.</param>
		public void RemoveEntity(Entity entity)
		{
			if (!entitiesToRemove.Contains(entity) && entities.Contains(entity))
				entitiesToRemove.Add(entity);
		}

		/// <summary>
		/// Adds multiple entities to the scene. The entities will be added at the beginning of the next update cycle.
		/// </summary>
		/// <param name="listedEntities">The entities to add to the scene.</param>
		protected void AddEntities(params Entity[] listedEntities)
		{
			foreach (Entity entity in listedEntities) {
				AddEntity(entity);
			}
		}

		/// <summary>
		/// Removes multiple entities from the scene. The entities will be removed at the beginning of the next update cycle.
		/// </summary>
		/// <param name="listedEntities">The entities to remove from the scene.</param>
		protected void RemoveEntities(params Entity[] listedEntities)
		{
			foreach (Entity entity in listedEntities)
			{
				RemoveEntity(entity);
			}

		}

		/// <summary>
		/// Processes any pending additions or removals of entities. This method should be called once per update cycle.
		/// </summary>
		private void ProcessEntityChanges()
		{
			foreach (var entity in entitiesToAdd)
			{
				entities.Add(entity);
				Game.Components.Add(entity);

				//If the entity has a transform, if it isn't the root entity and if it doesn't have a parent then add it as a parent to RootEntity. aefwef
				if (entity.HasComponent<Transform>() && entity != RootEntity && entity.GetComponent<Transform>().Parent == null)
					entity.GetComponent<Transform>().Parent = RootEntity.GetComponent<Transform>();
			}

			entitiesToAdd.Clear();

			foreach (var entity in entitiesToRemove)
			{
				entity.RemoveAllComponents();
				entities.Remove(entity);
				Game.Components.Remove(entity);
			}

			entitiesToRemove.Clear();
		}

		public override void Update(GameTime gameTime)
		{
			ProcessEntityChanges();

			base.Update(gameTime);	
		}


		private void SetVisibilityAndEnabled(bool enable)
		{
			foreach (Entity entity in entities)
			{
				entity.Enabled = entity.Visible = enable;
			}

			Enabled = Visible = enable;
		}

		/// <summary>
		/// Activates the scene, making all entities visible and enabled.
		/// </summary>
		public void Activate()
		{
			SetVisibilityAndEnabled(true); 
		}

		/// <summary>
		/// Deactivates the scene, making all entities invisible and disabled.
		/// </summary>
		public void Deactivate()
		{
			SetVisibilityAndEnabled(false); 
		}


	

	}
}
