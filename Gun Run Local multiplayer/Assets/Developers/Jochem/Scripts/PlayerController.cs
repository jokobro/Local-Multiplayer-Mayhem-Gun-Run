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
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();

        if (isGunner)
        {
            // Acties voor de gunner (bijv. richten en schieten).
        }
        else
        {
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
             _rigidBody.velocity = new Vector2 (_rigidBody.velocity.x, JumpForce);
             _isGrounded = false;
              Debug.Log("Player jump");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if( collision.gameObject.tag == ("Ground"))
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

}
