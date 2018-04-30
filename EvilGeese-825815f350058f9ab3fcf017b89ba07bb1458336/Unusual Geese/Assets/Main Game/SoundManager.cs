using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [EXTENSIONS] - New class to manage background music and sound effects
/// A script to manage all background music and sound effects in the game
/// </summary>
public class SoundManager : MonoBehaviour {

	public AudioSource BGMSource;
	public AudioSource SFXSource;
	public static SoundManager instance = null;
	private IDictionary<string, AudioClip> soundEffects;
	private IDictionary<string, AudioClip> backgroundMusic;

	/// <summary>
	/// Setup object and load all sound effects into <see cref="soundEffects"/> dictionary 
	/// </summary>
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);

		//Setup Sound Effects Dictionary
		soundEffects = new Dictionary<string, AudioClip> ();
		soundEffects.Add ("transition", Resources.Load ("Audio/transition", typeof(AudioClip)) as AudioClip);
		soundEffects.Add ("interact", Resources.Load ("Audio/interact", typeof(AudioClip)) as AudioClip);

		//Setup Background Music Dictionary
		backgroundMusic = new Dictionary<string, AudioClip> ();
		backgroundMusic.Add ("main", Resources.Load ("Audio/bgm", typeof(AudioClip)) as AudioClip);
		backgroundMusic.Add ("battle", Resources.Load ("Audio/battle", typeof(AudioClip)) as AudioClip);
		backgroundMusic.Add ("victory", Resources.Load ("Audio/victory", typeof(AudioClip)) as AudioClip);
		backgroundMusic.Add ("minigame", Resources.Load ("Audio/minigame", typeof(AudioClip)) as AudioClip);
	}

	/// <summary>
	/// Plays a background track
	/// </summary>
	/// <param name="music">The name of the background music to play</param>
	public void playBGM(string music) {
		BGMSource.clip = backgroundMusic [music];
		BGMSource.Play ();
	}

	/// <summary>
	/// Play a sound effect
	/// </summary>
	/// <param name="SFX">The name of the sound effect to reference within <see cref="soundEffects"/> </param>
	public void playSFX(string SFX) {
		SFXSource.clip = soundEffects [SFX];
		SFXSource.Play ();
	}

	/// <summary>
	/// Turn sound on and off
	/// </summary>
	public void soundOn() {
		BGMSource.mute = !BGMSource.mute;
		SFXSource.mute = !SFXSource.mute;
	}

}
