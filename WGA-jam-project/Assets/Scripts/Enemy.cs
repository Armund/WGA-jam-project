using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float speed = 10f;
    public float movementAreaRadius = 50f;
    public float movementAreaCenterX;
    public float movementAreaCenterZ;
    public float projectileSpeed = 10f;

    private float dist;
    private Vector3 targetPos;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Transform playerTransform;
    public GameObject projectile;
    public GameObject energyPrefab;

    void Start()
    {
        currentHealth = maxHealth;

        playerTransform = GameObject.Find("Player").transform;

        SetNextWaypoint();

        timeBtwShots = startTimeBtwShots;
    }

    void Update()
    {
        transform.LookAt(playerTransform.position);

        dist = Vector3.Distance(transform.position, targetPos);

        GoToWaypoint();
        Shoot();

        if (dist<1f) {
            SetNextWaypoint();
        }

    }

    public void GoToWaypoint() {

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            
    }

    public void SetNextWaypoint() {

        targetPos = Random.insideUnitSphere * movementAreaRadius;
        targetPos = new Vector3(movementAreaCenterX+targetPos.x, 1, movementAreaCenterZ+targetPos.z);

    }

    public void Shoot() {

        if (timeBtwShots <= 0)
        {
            projectile = Instantiate(projectile, transform.position, transform.rotation);
            Vector3 direction = projectile.transform.forward;
            projectile.GetComponent<Rigidbody>().AddForce(direction * projectileSpeed);
            timeBtwShots = startTimeBtwShots;

        }
        else {

            timeBtwShots -= Time.deltaTime;
        
        }

    }

    public void DropEnergy() {

        Instantiate(energyPrefab, transform.position, transform.rotation);

    }

    public void TakeDamage(float dmg) {
        currentHealth -= dmg;

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }

    }
}
