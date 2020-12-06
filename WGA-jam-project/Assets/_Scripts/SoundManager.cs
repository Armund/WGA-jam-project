using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public enum Sounds {MENU_THEME, MAIN_THEME, SHOT, SHIELD_REFLECTION, SHIELD, ENEMY_EXPLOSION, BULLET_DESTROY }

	public static SoundManager instance = null;

	public AudioSource[] audioSources = new AudioSource[10];

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
