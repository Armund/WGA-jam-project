using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    public float speed = 10f;
    //public float movementAreaRadius = 50f;
    public float movementAreaWidth;
    public float movementAreaHeight;
    public float movementAreaCenterX;
    public float movementAreaCenterZ;
    public int amountToDrop;

    private float dist;
    private Vector3 targetPos;

    public float timeBtwShots;
    public float startTimeBtwShots;

    private Transform playerTransform;
    public GameObject bulletPrefab;
    public GameObject energyPrefab;

    public GameObject gameField;
    
    private bool active;

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
        active = (gameController.activeGameField == gameField);

        if (active)
        {
            transform.LookAt(playerTransform.position);

            dist = Vector3.Distance(transform.position, targetPos);

            GoToWaypoint();
            Shoot();

            if (dist < 1f)
            {
                SetNextWaypoint();
            }
        }
    }

    public void GoToWaypoint() {

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            
    }

    public void SetNextWaypoint() {

        float randX = Random.Range(movementAreaCenterX - movementAreaWidth/2, movementAreaCenterX + movementAreaWidth/2);
        float randZ = Random.Range(movementAreaCenterZ - movementAreaHeight/2, movementAreaCenterZ + movementAreaHeight/2);

        //targetPos = Random.insideUnitSphere * movementAreaRadius;
        //targetPos = new Vector3(movementAreaCenterX+targetPos.x, 1, movementAreaCenterZ+targetPos.z);
        targetPos = new Vector3(randX, 1, randZ);
    }

    public void Shoot() {

        if (timeBtwShots <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.GetChild(0).position, transform.rotation); //TODO change second argument for actual enemy models
            Vector3 targetPosFixY = new Vector3(playerTransform.position.x, 1, playerTransform.position.z);
            bullet.GetComponent<BulletScript>().SetInitTarget(targetPosFixY);
            timeBtwShots = startTimeBtwShots;
			SoundManager.instance.PlaySound(SoundManager.Sounds.SHOT);
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



    public void DropEnergy(int amount) {
        for (int i = 0; i < amount; ++i)
            gameController.CreateEnergySphere(transform.position + Random.insideUnitSphere);
    }

    public void TakeDamage(float dmg) {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
			SoundManager.instance.PlaySound(SoundManager.Sounds.ENEMY_EXPLOSION);
            DropEnergy(amountToDrop);
            Destroy(gameObject);
        }

    }
}
