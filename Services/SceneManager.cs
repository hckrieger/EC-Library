﻿using EC.CoreSystem;
using EC.Services.AssetManagers;
using Microsoft.Xna.Framework;

namespace EC.Services
{
	/// <summary>
	/// Manages the scenes within the game, handling the transitions and lifecycle of each scene.
	/// </summary>
	public class SceneManager : IService 
    {
        private Dictionary<string, Scene> scenes;
        public Scene CurrentScene { get; set; }
        private Game game;


		/// <summary>
		/// Initializes a new instance of the SceneManager class.
		/// </summary>
		/// <param name="game">The game instance to which this SceneManager belongs.</param>
		public SceneManager(Game game) 
        {
            scenes = new Dictionary<string, Scene>();
            this.game = game;
        }

		/// <summary>
		/// Adds a scene to the SceneManager.
		/// </summary>
		/// <param name="name">The unique name used to identify the scene.</param>
		/// <param name="scene">The scene to be added.</param>
		public void AddScene(string name, Scene scene)
        {
            scene.ID = name;
            scenes[name] = scene;
			game.Components.Add(scene);
			scenes[name].Initialize();
            scenes[name].Deactivate();
		}

		/// <summary>
		/// Changes the current scene to a new scene.
		/// </summary>
		/// <param name="name">The name of the scene to change to.</param>
		/// <param name="shouldUnloadAndRemoveCurrent">Whether to unload and remove the current scene.</param>
		public void ChangeScene(string name)
        {
            if (!scenes.ContainsKey(name))
                throw new ArgumentException($"No scene with the name {name} exists");

            if (CurrentScene != null)
            {
				//if (shouldUnloadAndRemoveCurrent)
				//    UnloadAndRemoveCurrentScene();
				//else
				CurrentScene.Reset();
				CurrentScene.ProcessEntityChanges();
				CurrentScene.Deactivate();
			}

			

            CurrentScene = scenes[name];
			CurrentScene.Activate();  // Activate if it's already added but was inactive
			
		}



      }
}
