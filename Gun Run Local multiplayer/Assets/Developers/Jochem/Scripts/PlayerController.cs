using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   /* [Header("Player Number")]
    public int PlayerNumber; // Identificeert de speler

    [Header("Player")]// variable for player
    public float JumpForce = 5f;
    public float RunSpeed = 3f;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _shootAction;
    private Rigidbody _rigidBody;
    private bool _isGrounded;
    public bool BlockFireRatePickUp = false;
    public bool IsGunner = false;
    private PauseManager _pauseManager;
    private Gunner gunner;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Player" + PlayerNumber + "Movement"];
        _jumpAction = _playerInput.actions["Player" + PlayerNumber + "Jump"];
        _shootAction = _playerInput.actions.FindAction("Shoot");

        _jumpAction.performed += Jump;
        _rigidBody = GetComponent<Rigidbody>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Movement();
    }
    public void AssignGunner()
    {
        IsGunner = true;
        gameObject.SetActive(false); // Deactiveer de originele speler
        Debug.Log($"Player {PlayerNumber} is nu de gunner!");
    }

    private void Movement()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x * RunSpeed * Time.deltaTime, 0, 0);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        string expectedActionName = "Player" + PlayerNumber + "Jump";
        if (context.action.name != expectedActionName)
        {
            Debug.Log($"Player {PlayerNumber} ignored action {context.action.name}");
            return;
        }

        if (_isGrounded)
        {
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, JumpForce, _rigidBody.velocity.z);
            _isGrounded = false;
            Debug.Log($"Player {PlayerNumber} jumped!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        Debug.Log($"Player {PlayerNumber} died!");

    }
*/


 


    [Header("Player Number")]
    public int PlayerNumber; // Identificeert de speler

    [Header("Player Settings")] // Instellingen voor de speler
    public float JumpForce = 5f;
    public float RunSpeed = 3f;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Rigidbody _rigidBody;
    private bool _isGrounded;
    public bool BlockFireRatePickUp = false;
    public bool IsGunner = false;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Player" + PlayerNumber + "Movement"];
        _jumpAction = _playerInput.actions["Player" + PlayerNumber + "Jump"];

        _jumpAction.performed += Jump;
        _rigidBody = GetComponent<Rigidbody>();
        DontDestroyOnLoad(gameObject); // Zorg ervoor dat de speler niet vernietigd wordt bij scene overgang
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x * RunSpeed * Time.deltaTime, 0, 0);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, JumpForce, _rigidBody.velocity.z);
            _isGrounded = false;
            Debug.Log($"Player {PlayerNumber} jumped!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        Debug.Log($"Player {PlayerNumber} died!");
    }

    public void AssignGunner()
    {
        IsGunner = true;
        gameObject.SetActive(false); // Deactiveer de speler als de gunner wordt toegewezen
        Debug.Log($"Player {PlayerNumber} is now the gunner!");
    }
}


