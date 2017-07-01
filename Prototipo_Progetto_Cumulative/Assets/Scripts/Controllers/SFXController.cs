using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour
{

	public AudioMixerGroup SFXMixer;

	public AudioSource PlaySFX(AudioClip sfx, float volume = 1f, string name = "") 
	{
		AudioSource source = gameObject.AddComponent<AudioSource> ();
		source.outputAudioMixerGroup = SFXMixer;
		source.clip = sfx;
		source.volume = volume;
		source.name = name;
		source.Play ();
		StartCoroutine (StopSound (source));
		return source;
	}

	public IEnumerator StopSound(AudioSource source)
	{
		while (source.isPlaying) {
			yield return new WaitForSeconds (0.1f);
		}
		Destroy (source);
	}

	public void StopSound(string name)
	{
		List<AudioSource> audioList = new List<AudioSource> ();
		this.GetComponents<AudioSource> (audioList);
		foreach (AudioSource source in audioList) {
			if (source.name == name) {
				Destroy (source);
			}
		}
	}

	public bool IsPlaying(string name)
	{
		List<AudioSource> audioList = new List<AudioSource> ();
		this.GetComponents<AudioSource> (audioList);
		foreach (AudioSource source in audioList) {
			if (source.name == name) {
				return source.isPlaying;
			}
		}
		return false;
	}
}
