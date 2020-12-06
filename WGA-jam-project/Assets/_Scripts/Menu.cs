using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject mainPanel;

	public GameObject optionsPanel;

	public string gameSceneName;

	//Settings
	public Slider volumeSlider;
	public Text volumeValueText;

	// Start is called before the first frame update
	void Start() {
		if (!PlayerPrefs.HasKey("volume")) {
			PlayerPrefs.SetFloat("volume", 1);
		}
		SoundManager.instance.PlaySound(SoundManager.Sounds.MENU_THEME);
	}

	// Update is called once per frame
	void Update() {

	}

	public void StartGame() {
		SceneManager.LoadScene(gameSceneName);
	}

	public void OpenOptions() {
		mainPanel.SetActive(false);
		optionsPanel.SetActive(true);
		volumeSlider.value = PlayerPrefs.GetFloat("volume");
	}

	public void Exit() {
		Application.Quit();
	}

	public void BackToMenu() {
		SaveChanges();
		mainPanel.SetActive(true);
		optionsPanel.SetActive(false);
	}

	public void SaveChanges() {
		PlayerPrefs.SetFloat("volume", volumeSlider.value);
	}

	public void UpdateVolumeText() {
		float value = volumeSlider.value * 100;
		value = Mathf.Round(value);
		volumeValueText.text = value + "%";
	}
}
