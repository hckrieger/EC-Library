using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Services.AssetManagers
{
	/// <summary>
	/// Manages audio assets including sound effects and music, providing methods to play, pause, and stop sounds and music.
	/// </summary>
	public class AudioAssetManager : IService
	{

		private ContentManager content;
		private Dictionary<string, SoundEffect> soundEffectsCache = new Dictionary<string, SoundEffect>();
		private Dictionary<string, Song> musicCache = new Dictionary<string, Song>();

        public AudioAssetManager(ContentManager content)
        {
			this.content = content;
        }

		/// <summary>
		/// Loads a sound effect and caches it for future use.
		/// </summary>
		/// <param name="soundName">The asset name of the sound effect.</param>
		/// <returns>The SoundEffect instance.</returns>
		public SoundEffect LoadSoundEffect(string soundName)
		{
			if (!soundEffectsCache.TryGetValue(soundName, out SoundEffect soundEffect))
			{
				soundEffect = content.Load<SoundEffect>(soundName);
				soundEffectsCache[soundName] = soundEffect;
			}

			return soundEffect;
		}

		/// <summary>
		/// Plays a sound effect immediately.
		/// </summary>
		/// <param name="soundName">The name of the sound effect to play.</param>
		public void PlaySoundEffect(string soundName)
		{
			if (LoadSoundEffect(soundName) is SoundEffect soundEffect)
			{
				soundEffect.Play();
			}
		}

		/// <summary>
		/// Loads a music track and caches it for future use.
		/// </summary>
		/// <param name="musicName">The asset name of the music track.</param>
		/// <returns>The Song instance.</returns>
		public Song LoadMusic(string musicName)
		{
			if (!musicCache.TryGetValue(musicName, out Song song))
			{
				song = content.Load<Song>(musicName);
				musicCache[musicName] = song;	
			}

			return song;
		}

		/// <summary>
		/// Plays a music track. Only one music track can be active at a time.
		/// </summary>
		/// <param name="musicName">The name of the music track to play.</param>
		/// <param name="isRepeating">Indicates whether the music should loop.</param>
		public void PlayMusic(string musicName, bool isRepeating = true)
		{
			if (LoadMusic(musicName) is Song song)
			{
				MediaPlayer.IsRepeating = isRepeating;
				MediaPlayer.Play(song);
			}
		}


		/// <summary>
		/// Stops the currently playing music.
		/// </summary>
		public void StopMusic()
		{
			MediaPlayer.Stop();
		}

		/// <summary>
		/// Pauses the currently playing music.
		/// </summary>
		public void PauseMusic()
		{
			MediaPlayer.Pause();
		}

		/// <summary>
		/// Resumes paused music.
		/// </summary>
		public void ResumeMusic()
		{
			MediaPlayer.Resume();
		}

		/// <summary>
		/// Clears all cached audio assets. Useful when changing levels or scenes and the audio assets are no longer needed.
		/// </summary>
		public void ClearTotalAudioCache()
		{
			soundEffectsCache.Clear();
			musicCache.Clear();

			foreach (var sound in soundEffectsCache.Values)
				sound.Dispose();

			foreach (var music in musicCache.Values)
				music.Dispose();

		}

		public void UnloadAudioAsset(string assetName)
		{
			if (soundEffectsCache.ContainsKey(assetName))
			{
				soundEffectsCache[assetName].Dispose();
				soundEffectsCache.Remove(assetName);
			}

			if (musicCache.ContainsKey(assetName))
			{
				musicCache[assetName].Dispose();
				musicCache.Remove(assetName);
			}
		}
	}


}
