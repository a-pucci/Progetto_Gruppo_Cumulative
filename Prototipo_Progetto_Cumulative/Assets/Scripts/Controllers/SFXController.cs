using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour
{

	public AudioMixerGroup SFXMixer;

	public AudioSource PlaySFX(AudioClip sfx, float volume = 1f) 
	{
		Debug.Log (sfx.name);
		AudioSource source = gameObject.AddComponent<AudioSource> ();
		source.outputAudioMixerGroup = SFXMixer;
		source.clip = sfx;
		source.volume = volume;
		source.Play ();
		StartCoroutine (StopSound (source));
		return source;
	}

	public IEnumerator StopSound(AudioSource source)
	{
		while (source.isPlaying) {
			yield return new WaitForSeconds (0.1f);
		}
		Debug.Log ("Destroying");
		Destroy (source);
	}
}
