using EC.CoreSystem;
using EC.Services.AssetManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services
{
	/// <summary>
	/// Manages the scenes within the game, handling the transitions and lifecycle of each scene.
	/// </summary>
	public class SceneManager : IService 
    {
        private Dictionary<string, Scene> scenes;
        private Scene currentScene;
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
        }

		/// <summary>
		/// Changes the current scene to a new scene.
		/// </summary>
		/// <param name="name">The name of the scene to change to.</param>
		/// <param name="shouldUnloadAndRemoveCurrent">Whether to unload and remove the current scene.</param>
		public void ChangeScene(string name, bool shouldUnloadAndRemoveCurrent = false)
        {
            if (!scenes.ContainsKey(name))
                throw new ArgumentException($"No scene with the name {name} exists");

            if (currentScene != null)
            {
                if (shouldUnloadAndRemoveCurrent)
                    UnloadAndRemoveCurrentScene();
                else
                    currentScene.Deactivate();
			}

            currentScene = scenes[name];
            game.Components.Add(currentScene);
            currentScene.Activate();
         
        }

		/// <summary>
		/// Unloads and removes the current scene from the SceneManager.
		/// </summary>
		private void UnloadAndRemoveCurrentScene()
        {
            if (currentScene != null)
            {
                GraphicsAssetManager assetManager = game.Services.GetService<GraphicsAssetManager>();
                if (assetManager != null)
                {
                    assetManager.UnloadContent();
                }

                currentScene.Dispose();
                scenes.Remove(currentScene.ID);
                game.Components.Remove(currentScene);
            }
        }

    }
}
