using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMController : MonoBehaviour
{
	private	AudioSource source;
	private StageSwapController stageSwap;
	public AudioMixerGroup bgmMixerA;
	public AudioMixerGroup bgmMixerB;

	void Start()
	{
		GameObject stageObject = GameObject.FindGameObjectWithTag ("StageSwap");
		if (stageObject != null) {
			stageSwap = GetComponent<StageSwapController> ();
		}
		source = this.GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (stageSwap != null) {
			if (stageSwap.toggle) {
				source.outputAudioMixerGroup = bgmMixerB;
			} else {
				source.outputAudioMixerGroup = bgmMixerA;
			}
		}
	}
}
