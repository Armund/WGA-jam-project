using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;

    public float damage;

    public bool notDamaged;

    public Vector3 normalizedDirection;

    public void OnEnable()
    {
        normalizedDirection = transform.position;
        notDamaged = true;
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
}