using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float JumpForce = 5f;
    public float RunSpeed = 3f;
    
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Rigidbody _rigidBody;
    

    private bool _isGrounded;
    


    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("PlayerMovement");
        _jumpAction = _playerInput.actions.FindAction("Jump");
        _rigidBody = GetComponent<Rigidbody>();
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
    }

}
