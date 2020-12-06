using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
	public static Resources instance = null;
	public GameObject explosionEffect;
	// Start is called before the first frame update
	void Start()
    {
		instance = this;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
