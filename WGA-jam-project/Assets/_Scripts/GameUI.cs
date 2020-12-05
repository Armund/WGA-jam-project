using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public static GameUI instance = null;

	public Slider HpSlider;
	public Slider EnergySlider;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) {
			instance = this;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void UpdateHP(float newValue) {
		HpSlider.value = newValue;
	}

	public void UpdateEnergy(float newValue) {
		EnergySlider.value = newValue;
	}
}
