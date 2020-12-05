using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySphereScript : MonoBehaviour
{
    public float energyAmount;
    public float hpAmount;

    public bool notPicked;

    public float lifetime;

    public GameController gameController;

    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
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

    protected void OnDestroy()
    {
        gameController.currentSpheres -= 1;
    }
}
