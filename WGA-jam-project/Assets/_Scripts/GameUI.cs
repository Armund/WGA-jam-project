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
	bool isPaused = false;

	public Slider HpSlider;
	public Slider EnergySlider;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
			instance = this;
		}
		SoundManager.instance.PlaySound(SoundManager.Sounds.MAIN_THEME);
    }

	protected void Awake() {
		pauseControls = new PauseControls();
	}

	public void OnEnable() {
		pauseControls.Enable();
		pauseControls.UI.Cancel.performed += PauseHandler;
		pauseControls.UI.Cancel.Enable();
	}

	private void PauseHandler(InputAction.CallbackContext context) {
		PauseUnpause();
	}

	public void PauseUnpause() {
		if (!isPaused) {
			isPaused = true;
			Time.timeScale = 0;
			PauseMenu.SetActive(true);
		} else {
			isPaused = false;
			Time.timeScale = 1;
			PauseMenu.SetActive(false);
		}
	}

	public void LoadScene(string sceneName) {
		isPaused = false;
		Time.timeScale = 1;
		PauseMenu.SetActive(false);
		SceneManager.LoadScene(sceneName);
	}

	public void UpdateHP(float newValue) {
		HpSlider.value = newValue/100;
	}

	public void UpdateEnergy(float newValue) {
		EnergySlider.value = newValue/100;
	}
}
