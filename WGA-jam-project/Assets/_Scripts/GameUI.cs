using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
	[SerializeField] private PlayerControls pauseControls;

	public static GameUI instance = null;

	public GameObject PauseMenu;
	public GameObject WinScreen;
	bool isPaused;
	bool isGameOver;

	public Slider HpSlider;
	public Slider EnergySlider;

	public Text scoreTable;
	public Text scoreValuesText;

    // Start is called before the first frame update
    void Start()
    {
		Unpause();
		isGameOver = false;
		SoundManager.instance.PlaySound(SoundManager.Sounds.MAIN_THEME);
    }

	private void Update() {
		scoreTable.text = "Score: " + GameController.score + "\n" + "Time: " + Mathf.Round(Player.playTime);
	}

	protected void Awake() {
		if (instance == null) {
			instance = this;
		}
		pauseControls = new PlayerControls();
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
	
	public void Unpause() {
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

	public void WinGame() {
		Pause();
		isGameOver = true;
		PauseMenu.SetActive(false);
		WinScreen.SetActive(true);
		scoreValuesText.text = GameController.score + "\n" + Mathf.Round(Player.playTime) + "\n" + GetFinalScore();
	}

	public int GetFinalScore() {
		int result = 0;

		if (Mathf.Round(Player.playTime) < 180) {
			result = GameController.score * 3;
		} else if (Mathf.Round(Player.playTime) < 360) {
			result = GameController.score * 2;
		} else {
			result = GameController.score;
		}

		return result;
	}
}
