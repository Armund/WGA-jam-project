using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereScript : MonoBehaviour
{
    public float energyAmount;

    public bool notPicked;

    public float lifetime;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    public void OnEnable()
    {
        notPicked = true;
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
