using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
	public GameObject shieldModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Activate() {
		shieldModel.SetActive(true);
	}

	public void Disactivate() {
		shieldModel.SetActive(false);
	}

}
