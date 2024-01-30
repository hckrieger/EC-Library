
using EC.CoreSystem;
using EC.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EC.Components.Renderers
{
	/// <summary>
	/// Abstract base class for all renderers.  Provides common functionality for rendering
	/// </summary>
	public abstract class Renderer : Component
	{
		protected RenderManager renderManager;

		private Entity entity;

		/// <summary>
		/// The Color used for rendering.
		/// </summary>
		public Color Color { get; set; } = Color.White;

		/// <summary>
		/// The dpth layer fro rendering.
		/// </summary>
		public float LayerDepth { get; set; } = .5f;

		/// <summary>
		/// Effects to be applied during rendering, such as flipping.
		/// </summary>
		public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;



		/// <summary>
		/// Retrieves the Transform component of the entity associated with this renderer
		/// </summary>
		public Transform Transform
		{
			get { return Entity?.GetComponent<Transform>(); }
		}

		/// <summary>
		/// Retrieves the Origin component of the entity associated with this renderer
		/// </summary>
		public Origin Origin
		{
			get
			{
				if (Entity.HasComponent<Origin>())
					return Entity.GetComponent<Origin>();
				else
					return null;
			}
		}

		/// <summary>
		/// Initializes new instance of the Render class
		/// </summary>
		/// <param name="game">The game instance which the renderer belongs.</param>
		/// <param name="entity">The entity to which this renderer is attached.</param>
		public Renderer(Game game, Entity entity) : base(entity)
		{
			renderManager = game.Services.GetService<RenderManager>();
			this.entity = entity;
		}


		/// <summary>
		/// Checks if the entity is visible; visibility is set through the DrawableGameComponent class that the entity inherits from
		/// </summary>
		/// <returns></returns>
		protected bool IsEntityVisible()
		{
			return entity?.Visible ?? false;
		}

		/// <summary>
		/// Abstract draw method to be implemented by derived renderer classes.
		/// </summary>
		public abstract override void Draw();
	}
}
