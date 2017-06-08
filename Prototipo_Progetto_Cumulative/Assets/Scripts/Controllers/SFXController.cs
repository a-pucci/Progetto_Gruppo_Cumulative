using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour
{

	public AudioMixerGroup SFXMixer;

	public void PlaySFX(AudioClip sfx) 
	{
		Debug.Log (sfx.name);
		AudioSource source = gameObject.AddComponent<AudioSource> ();
		source.outputAudioMixerGroup = SFXMixer;
		source.clip = sfx;
		source.Play ();
		StartCoroutine (StopSound (source));

	}

	public IEnumerator StopSound(AudioSource source)
	{
		if (source.isPlaying) {
			yield return new WaitForSeconds (0.1f);
		} else {
			Destroy (source);
		}
	}
}
