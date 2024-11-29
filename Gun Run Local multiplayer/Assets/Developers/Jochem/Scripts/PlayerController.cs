using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private Rigidbody _rigidBody;
    private bool _isGrounded;
    public float JumpForce = 5f;
    public float RunSpeed = 3f;

    //variablen voor de gunner
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnpoint;
    private InputAction _shootAction;
    private float _nextfire;
    private float _bulletSpeed = 6f;
    public float FireRate = 3.0f;
    public bool BlockFireRatePickUP = false;
    public bool isGunner = false;

    public void AssignGunner()
    {
        isGunner = true;
        // Voer acties uit specifiek voor de gunner, zoals het activeren van een wapen.
    }

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("PlayerMovement");
        _shootAction = _playerInput.actions.FindAction("Shoot");
        _shootAction.performed += shoot;
        _rigidBody = GetComponent<Rigidbody>();



    }

    private void Update()
    {
        Movement();

        if (isGunner && _shootAction.triggered)
        {
            ShootBullet();
        }
        else
        {
            Movement();

            // Acties voor de runners (bijv. bewegen en ontwijken).
        }

    }

    private void Movement()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x * RunSpeed * Time.deltaTime, 0, 0);
    }

    public void Jump(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            if (_isGrounded == true)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpForce);
                _isGrounded = false;
                Debug.Log("Player jump");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = true;
        }
        if (collision.gameObject.tag == ("Projectile"))
        {
            Death();
        }
    }

    private void Death()
    {



    }




    public void shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootBullet(); // Roep een generieke schietfunctie aan.
        }
    }

    private void ShootBullet()
    {
        if (BlockFireRatePickUP == true)
        {
            Debug.Log("Cant fire");
            return;
        }

        if (Time.time > _nextfire)
        {
            _nextfire = Time.time + FireRate;
            var bullet = Instantiate(_bulletPrefab, _bulletSpawnpoint.position, _bulletSpawnpoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnpoint.forward * _bulletSpeed;
            Debug.Log("Bullet fired!");
        }
    }
}
