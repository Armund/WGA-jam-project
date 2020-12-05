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

    protected void Awake()
    {
        playerControls = new PlayerControls();
    }

    public void OnEnable()
    {
        shield = GameObject.Find("Shield"); // GetComponent<Shield>();
        shield.SetActive(false);

        playerControls.Enable();
        playerControls.Player.Look.performed += LookHandler;
        playerControls.Player.Look.Enable();
        playerControls.Player.Move.performed += MoveHandler;
        playerControls.Player.Move.Enable();
        playerControls.Player.Fire.performed += FireHandler;
        playerControls.Player.Fire.Enable();
    }

    public void onDisable()
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
        
    }

    protected void Update()
    {
        Move(playerDirection);
        Turn(mousePosition);
        DrawCursor(mousePosition);
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
            shieldUp(); // shield.Up(); 
        else
            shieldDown(); // shield.Down(); 
    }

    private void shieldUp()
    {
        Debug.Log("Shield up!");
        shield.SetActive(true);
    }

    private void shieldDown()
    {
        Debug.Log("Shield down!");
        shield.SetActive(false);
    }
}