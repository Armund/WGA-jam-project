using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;

    public float damage;

    public bool notDamaged;

    public bool reflected;

    public float lifetime;

    public Material justShot;

    public Material justReflected;

    public Vector3 normalizedDirection;

    void Awake()
    {
        Destroy(gameObject, lifetime);
    }

    public void OnEnable()
    {
        GetComponent<Renderer>().material = justShot;
        normalizedDirection = transform.position;
        notDamaged = true;
        reflected = false;
    }

    public void SetInitTarget(Vector3 target)
    {
        normalizedDirection = (target - transform.position).normalized;
    }

    public void SetTarget(Vector3 target)
    {
        normalizedDirection = target;
    }

    public void Damage()
    {
        notDamaged = false;
        Destroy(gameObject);
    }

    protected void Update()
    {
        transform.position += normalizedDirection * speed * Time.deltaTime;
    }

    public void ChangeTexture()
    {
        GetComponent<Renderer>().material = justReflected;
    }
}