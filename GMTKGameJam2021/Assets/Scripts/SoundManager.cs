using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public Sound[] sounds;

	// Audio players components.
	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	// Random pitch adjustment range.
	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	// Singleton instance.
	public static SoundManager Instance = null;

	private Dictionary<string, AudioSource> _sources;
	private Dictionary<string, Sound> _sounds;

	// Initialize the singleton instance.
	void Awake()
	{
		_sources = new Dictionary<string, AudioSource>();
		_sounds = new Dictionary<string, Sound>();

		// If there is not already an instance of SoundManager, set it to this.
		if (Instance == null)
		{
			Instance = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

		foreach (var sound in sounds)
		{
			if (_sounds.ContainsKey(sound.clip.name))
            {
				continue;
            }

			_sounds.Add(sound.clip.name, sound);	
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);
	}

	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip)
	{
		if (!_sources.ContainsKey(clip.name))
        {
			AudioSource s;
			
			if (_sounds.ContainsKey(clip.name))
            {
				s = BuildAudioSource(_sounds[clip.name]);
            } else
            {
				s = BuildAudioSource(clip);
			}

			s.transform.parent = gameObject.transform;
			_sources.Add(clip.name, s);
		}

		var source = _sources[clip.name];

		if (!source.isPlaying)
        {
			_sources[clip.name].Play();
		}
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}

	public void StopMusic()
    {
		MusicSource.Stop();
    }

	private AudioSource BuildAudioSource(AudioClip clip)
	{
		var child = new GameObject();

		var source = child.AddComponent<AudioSource>();
		source.clip = clip;

		source.volume = 1f;

		return source;
	}

	private AudioSource BuildAudioSource(Sound sound)
	{
		var child = new GameObject();

		var source = child.AddComponent<AudioSource>();
		source.clip = sound.clip;

		source.volume = sound.volume;

		return source;
	}
}