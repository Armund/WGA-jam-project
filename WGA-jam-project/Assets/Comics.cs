using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Comics : MonoBehaviour
{
	public Image image;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Sprite sprite4;
	private int currentSprite;

    // Start is called before the first frame update
    void Start()
    {
		image.sprite = sprite1;
		currentSprite = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Next() {
		switch (currentSprite) {
			case 1:
				image.sprite = sprite2;
				currentSprite++;
				break;
			case 2:
				image.sprite = sprite3;
				currentSprite++;
				break;
			case 3:
				image.sprite = sprite4;
				currentSprite++;
				break;
			case 4:
				SceneManager.LoadScene("Game_Scene");
				break;
		}
	}
}
