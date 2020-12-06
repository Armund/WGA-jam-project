using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls;

    public float playerSpeed;

    public float rotateSpeed;

    public Transform mouseCursor;

    private Vector2 playerDirection;

    private float playerAngle;

    private Vector3 mousePosition;

    private GameObject shield; // private Shield shield;

    private float health;

    public float maxHealth;

    public float energy;

    public float maxEnergy;

    public float energyDrain;

    protected void Awake()
    {
        playerControls = new PlayerControls();
    }

    public void OnEnable()
    {
        shield = GameObject.Find("Shield");
        
        health = maxHealth;
        energy = maxEnergy;
		GameUI.instance.UpdateHP(health);
		GameUI.instance.UpdateEnergy(energy);

		playerControls.Enable();
        playerControls.Player.Look.performed += LookHandler;
        playerControls.Player.Look.Enable();
        playerControls.Player.Move.performed += MoveHandler;
        playerControls.Player.Move.Enable();
        playerControls.Player.Fire.performed += FireHandler;
        playerControls.Player.Fire.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
        playerControls.Player.Look.performed -= LookHandler;
        playerControls.Player.Look.Disable();
        playerControls.Player.Move.performed -= MoveHandler;
        playerControls.Player.Move.Disable();
        playerControls.Player.Fire.performed -= FireHandler;
        playerControls.Player.Fire.Disable();
    }

    protected void Start()
    {

        shield.GetComponent<Shield>().Disactivate();
        shield.SetActive(false);

    }

    protected void Update()
    {
        Move(playerDirection);
        Turn(mousePosition);
        DrawCursor(mousePosition);      

        if (shield.activeInHierarchy) DrainShield();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<EnergySphereScript>())
            if (collider.gameObject.GetComponent<EnergySphereScript>().notPicked)
            {
                collider.gameObject.GetComponent<EnergySphereScript>().Pick();
                float energyAmount = collider.gameObject.GetComponent<EnergySphereScript>().energyAmount;
                float hpAmount = collider.gameObject.GetComponent<EnergySphereScript>().hpAmount;
                if ((energyAmount + energy) <= maxEnergy)
                    energy += energyAmount;
                else
                    energy = maxEnergy;
                if ((hpAmount + health) <= maxHealth)
                    health += hpAmount;
                else
                    health = maxHealth;
            }
            else
                Debug.Log("Tried to pick up same energy sphere more than once");
        else if (collider.gameObject.GetComponent<BulletScript>())
            if (collider.gameObject.GetComponent<BulletScript>().notDamaged)
            {
                collider.gameObject.GetComponent<BulletScript>().Damage();
                float damageToDeal = collider.gameObject.GetComponent<BulletScript>().damage;
                if ((health - damageToDeal) <= 0)
                    BeKilled();
                else
                    health -= damageToDeal;
            }
            else
                Debug.Log("Tried to recive damage from the same bullet more than once");
        else if (collider.gameObject.GetComponent<Enemy>())
            BeKilled();
        else if (collider.gameObject.GetComponent<DoorScript>())
            BeKilled();
        else
            Debug.Log("Unknown collider");
		GameUI.instance.UpdateHP(health);
		GameUI.instance.UpdateEnergy(energy);
	}

    private void BeKilled()
    {
        Debug.Log("Game over!");
        Destroy(gameObject);
    }

    private void LookHandler(InputAction.CallbackContext context)
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        float hitdist = 0.0f;
        playerPlane.Raycast(ray, out hitdist);
        mousePosition = ray.GetPoint(hitdist);
    }

    private void MoveHandler(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>().normalized;
    }
   
    private void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, 0, direction.y) * playerSpeed * Time.deltaTime;
    }

    private void Turn(Vector3 position)
    {
        Vector3 targetDirection = position - transform.position;
        float singleStep = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        newDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void DrawCursor(Vector3 position)
    {
        mouseCursor.position = position;
    }

    private void FireHandler(InputAction.CallbackContext context)
    {
        var buttonPressed = context.ReadValue<float>();
        if (buttonPressed == 1)
            ShieldUp(); // shield.Up(); 
        else
            ShieldDown(); // shield.Down(); 
    }

    private void ShieldUp()
    {
        if (energy > 0)
        {
            Debug.Log("Shield up!");
            shield.GetComponent<Shield>().Activate();
            shield.SetActive(true);
        }
    }

    private void ShieldDown()
    {
        Debug.Log("Shield down!");
        shield.GetComponent<Shield>().Disactivate();
        shield.SetActive(false);
    }

    private void DrainShield()
    {
        energy -= energyDrain * Time.deltaTime;
		GameUI.instance.UpdateEnergy(energy);
        if (energy <= 0)
        {
            energy = 0;
            Debug.Log("No more energy!");
            ShieldDown();
        }
    }
}