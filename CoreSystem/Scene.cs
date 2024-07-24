using EC.Components;
using EC.Components.Render;
using EC.Services.AssetManagers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		private GraphicsAssetManager graphicsAssetManager;


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

			graphicsAssetManager = game.Services.GetService<GraphicsAssetManager>();	
		}

		

		/// <summary>
		/// Adds a single entity to the scene. The entity will be added at the beginning of the next update cycle.
		/// </summary>
		/// <param name="entity">The entity to add to the scene.</param>
		protected void AddEntity(Entity entity)
		{
			if (!entitiesToAdd.Contains(entity) 
				&& !entities.Contains(entity))
			{
				entitiesToAdd.Add(entity);
				entity.Initialize();
			}
				
		}

		/// <summary>
		/// Adds the entity to the scene but also set its parent
		/// </summary>
		/// <param name="child">The child entity to be added</param>
		/// <param name="parent">The parent entity that the child is being assigned to.</param>
		protected void AddEntity(Entity child, Entity parent)
		{
			AddEntity(child);
			child.Transform.Parent = parent.Transform;
			child.DefaultParent = parent;
			child.Initialize();
		}

		/// <summary>
		/// Removes a single entity from the scene. The entity will be removed at the beginning of the next update cycle.
		/// </summary>
		/// <param name="entity">The entity to remove from the scene.</param>
		protected void RemoveEntity(Entity entity, bool disposeAsset = false)
		{
			if (!entitiesToRemove.Contains(entity) && entities.Contains(entity))
			{
				Debug.WriteLine("add to remove list");
				entitiesToRemove.Add(entity);

				if (disposeAsset)
				{
					if (entity.HasComponent<TextureRenderer>())
						graphicsAssetManager.UnloadGraphicsAsset(entity.GetComponent<TextureRenderer>().TextureName);
					

					if (entity.HasComponent<TextRenderer>())
						graphicsAssetManager.UnloadGraphicsAsset(entity.GetComponent<TextRenderer>().FontName);
					
				}

			}
				
		}




		/// <summary>
		/// Processes any pending additions or removals of entities. This method should be called once per update cycle.
		/// </summary>
		public void ProcessEntityChanges()
		{
			foreach (var entity in entitiesToAdd)
			{
				entities.Add(entity);
				//Game.Components.Add(entity);

		
			}

			entitiesToAdd.Clear();

			foreach (var entity in entitiesToRemove)
			{ 




				entity.RemoveAllComponents();
				entities.Remove(entity);

				Debug.WriteLine("Entities being removed");
				
			}

			entitiesToRemove.Clear();
		}

		public override void Initialize()
		{

			foreach (var entity in entities)
			{
				entity.Initialize();
			}

			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			ProcessEntityChanges();



			foreach (var entity in entities)
			{
				if (entity.Enabled)
					entity.Update(gameTime);
			}

			base.Update(gameTime);	
		}



		public override void Draw(GameTime gameTime)
		{

			foreach (var entity in entities)
			{
				if (entity.Visible)
				{
					entity.Draw(gameTime);
				}
			}

			base.Draw(gameTime);


		}


		public virtual void Reset()
		{

		}


		/// <summary>
		/// Activates the scene, making all entities visible and enabled.
		/// </summary>
		public void Activate()
		{
			SetVisibilityAndEnable(true);

		}

		/// <summary>
		/// Deactivates the scene, making all entities invisible and disabled.
		/// </summary>
		public void Deactivate()
		{
			SetVisibilityAndEnable(false);
		}


		private void SetVisibilityAndEnable(bool visible)
		{
			foreach (var entity in entities)
			{ 
				if (visible)
				{
					entity.Visible = entity.IntendedVisible;
					entity.Enabled = entity.IntendedEnable;
				} else
				{
					entity.Visible = false;
					entity.Enabled = false;
				}

			}

			Enabled = Visible = visible;
		}

	}
}
