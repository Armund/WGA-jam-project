using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereScript : MonoBehaviour
{
    public float energyAmount;

    public bool notPicked;
    
    protected void Start()
    {
        
    }

    public void OnEnable()
    {
        notPicked = true;
    }

    void OnTriggerEnter(Collider col)
    {

    }

    public void Pick()
    {
        notPicked = false;
        Destroy(gameObject);
    }

    protected void Update()
    {

    }
}
