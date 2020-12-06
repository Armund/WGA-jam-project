using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
	[SerializeField] private PauseControls pauseControls;

	public static GameUI instance = null;

	public GameObject PauseMenu;
	bool isPaused;
	bool isGameOver;

	public Slider HpSlider;
	public Slider EnergySlider;

    // Start is called before the first frame update
    void Start()
    {
		isPaused = false;
		isGameOver = false;
		SoundManager.instance.PlaySound(SoundManager.Sounds.MAIN_THEME);
    }

	protected void Awake() {
		if (instance == null) {
			instance = this;
		}
		pauseControls = new PauseControls();
	}

	public void OnEnable() {
		pauseControls.Enable();
		pauseControls.UI.Cancel.performed += PauseHandler;
		pauseControls.UI.Cancel.Enable();
	}

	private void PauseHandler(InputAction.CallbackContext context) {
		if (isPaused) {
			Unpause();
		} else {
			Pause();
		}
	}

	private void Pause() {
		isPaused = true;
		Time.timeScale = 0;
		PauseMenu.SetActive(true);
	}
	
	private void Unpause() {
		if (!isGameOver) {
			isPaused = false;
			Time.timeScale = 1;
			PauseMenu.SetActive(false);
		}
	}

	public void LoadScene(string sceneName) {
		Unpause();
		SceneManager.LoadScene(sceneName);
	}

	public void UpdateHP(float newValue) {
		HpSlider.value = newValue/100;
	}

	public void UpdateEnergy(float newValue) {
		EnergySlider.value = newValue/100;
	}

	public void GameOver() {
		Pause();
		isGameOver = true;
	}
}
