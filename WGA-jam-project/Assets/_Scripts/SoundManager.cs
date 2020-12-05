using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public enum Sounds {MAIN_THEME, SHOOT, SHIELD_REFLECTION, SHIELD_UP, SHIELD_DOWN, EXPLOSION, WALK }

	public static SoundManager instance = null;

	public AudioSource[] audioSources = new AudioSource[20];

	// Start is called before the first frame update
	void Start()
    {
		if (instance == null) {
			instance = this;
		}
	}

	public void PlaySound(Sounds sound) {
		audioSources[(int)sound].Play();
	}

	public void StopSound(Sounds sound) {
		audioSources[(int)sound].Stop();
	}
}
