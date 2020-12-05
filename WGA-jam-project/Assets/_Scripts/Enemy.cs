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

    public float timeBtwShots;
    public float startTimeBtwShots;

    private Transform playerTransform;
    public GameObject bulletPrefab;
    public GameObject energyPrefab;

    public GameController gameController;

    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

    }

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
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.GetChild(1).position, transform.rotation); //TODO change second argument for actual enemy models
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
        gameController.CreateEnergySphere(transform.position + Random.insideUnitSphere);
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
