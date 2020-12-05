using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public Material fine;

    public Material damaged;

    public GameController gameController;

    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }
   
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<BulletScript>())
            if (collider.gameObject.GetComponent<BulletScript>().reflected)
                if (collider.gameObject.GetComponent<BulletScript>().notDamaged)
                {
                    collider.gameObject.GetComponent<BulletScript>().Damage();
                    float damageToDeal = collider.gameObject.GetComponent<BulletScript>().damage;
                    TakeDamage(damageToDeal);
                }
                else
                    Debug.Log("Tried to recive damage from the same bullet more than once");
            else
            {
                collider.gameObject.GetComponent<BulletScript>().Damage();
                Debug.Log("Bullet couldn't damage the Door, because it was not charged by shield!");
            }
        else
            Debug.Log("Unknown collider");
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (0 < currentHealth && currentHealth <= 50)
        {
            ChangeTexture();
        } else if (currentHealth <= 0)
        { 
            Destroy(gameObject);
        }

    }

    public void ChangeTexture()
    {
        GetComponent<Renderer>().material = damaged;
    }
}
