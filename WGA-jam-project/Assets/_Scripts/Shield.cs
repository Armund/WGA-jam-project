﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Vector3 velocity;

    private bool active;

    void Start()
    {
        active = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("col");
        ReflectProjectile(collision.rigidbody, collision.contacts[0].normal);
    }

    private void ReflectProjectile(Rigidbody rb, Vector3 reflectVector)
    {
        velocity = Vector3.Reflect(velocity, reflectVector);
        rb.velocity = velocity;
    }

    void OnTriggerEnter(Collider collider)
    {
        Vector3 normal = transform.forward;
        Vector3 reflected = Vector3.Reflect(collider.GetComponent<BulletScript>().normalizedDirection*(10^6), normal).normalized;
        reflected.y = 0;
        collider.gameObject.GetComponent<BulletScript>().SetTarget(reflected);
    }


    void Update()
    {
        
    }

	public void Activate()
    {
        active = true;
    }

	public void Disactivate()
    {
        active = false;
    }

    public bool IsActive()
    {
        return active;
    }
}
