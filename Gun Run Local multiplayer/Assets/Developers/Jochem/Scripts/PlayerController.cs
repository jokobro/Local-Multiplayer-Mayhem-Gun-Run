using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _shootAction;
    private Rigidbody _rigidBody;
    private bool _isGrounded;

    public float JumpForce = 5f;
    public float RunSpeed = 3f;

    // Variabelen voor de gunner
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnpoint;
    private float _nextFire;
    private float _bulletSpeed = 6f;
    public float FireRate = 3.0f;
    public bool BlockFireRatePickUp = false;
    public bool IsGunner = false;

    public int PlayerNumber = 1; // Identificeert de speler (bijv. 1, 2, 3, etc.)

    public void AssignGunner()
    {
        IsGunner = true;
        // Specifieke acties voor de gunner, zoals het activeren van een wapen.
    }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Player" + PlayerNumber + "Movement"];
        _jumpAction = _playerInput.actions["Player" + PlayerNumber + "Jump"];
        _shootAction = _playerInput.actions["Player" + PlayerNumber + "Shoot"];
        _shootAction.performed += Shoot;
        _jumpAction.performed += Jump;

        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();

        if (IsGunner && _shootAction.triggered)
        {
            ShootBullet();
        }
    }

    private void Movement()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x * RunSpeed * Time.deltaTime, 0, 0);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && _isGrounded)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
            _isGrounded = false;
            Debug.Log($"Player {PlayerNumber} jump");
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

    private void Death()
    {
        Debug.Log($"Player {PlayerNumber} died!");
        // Implementeer doodslogica (bijv. respawn, game over, etc.).
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        if (BlockFireRatePickUp)
        {
            Debug.Log($"Player {PlayerNumber} can't fire due to FireRate block.");
            return;
        }

        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + FireRate;
            var bullet = Instantiate(_bulletPrefab, _bulletSpawnpoint.position, _bulletSpawnpoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnpoint.forward * _bulletSpeed;
            Debug.Log($"Player {PlayerNumber} fired a bullet!");
        }
    }
}
