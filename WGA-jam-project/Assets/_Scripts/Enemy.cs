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

    private float dist;
    private Vector3 targetPos;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private Transform playerTransform;
    public GameObject bulletPrefab;
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
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.localPosition, transform.rotation);
            bullet.GetComponent<BulletScript>().SetInitTarget(playerTransform.position);
            timeBtwShots = startTimeBtwShots;
        }
        else {

            timeBtwShots -= Time.deltaTime;
        
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<BulletScript>())
            if (collider.gameObject.GetComponent<BulletScript>().notDamaged)
            {
                collider.gameObject.GetComponent<BulletScript>().Damage();
                float damageToDeal = collider.gameObject.GetComponent<BulletScript>().damage;
                TakeDamage(damageToDeal);
            }
            else
                Debug.Log("Tried to recive damage from the same bullet more than once");
        else
            Debug.Log("Unknown collider");
    }



    public void DropEnergy() {
        GameObject energy = Instantiate(energyPrefab, transform.position + Random.insideUnitSphere, transform.rotation);
    }

    public void TakeDamage(float dmg) {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            DropEnergy();
            DropEnergy();
            DropEnergy();
            Destroy(gameObject);
        }

    }
}
