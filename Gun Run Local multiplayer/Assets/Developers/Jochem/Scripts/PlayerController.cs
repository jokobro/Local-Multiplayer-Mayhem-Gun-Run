using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Number")]
    public int PlayerNumber; // Identificeert welke speler dit script bestuurt.

    [Header("Player Settings")] // Instellingen voor de speler.
    public float JumpForce = 5f; // Kracht waarmee de speler springt.
    public float RunSpeed = 3f; // Snelheid waarmee de speler rent.
    private PlayerInput _playerInput; // Verwijzing naar het PlayerInput-component.
    private InputAction _moveAction; // Input voor beweging.
    private InputAction _jumpAction; // Input voor springen.
    private Rigidbody _rigidBody; // Verwijzing naar de Rigidbody-component.
    private bool _isGrounded; // Controle of de speler op de grond staat.
    public bool BlockFireRatePickUp = false; // Controle of pickup actief is. 
    public bool IsGunner = false; // Controle of de speler de "gunner" rol heeft.

    private void Start()
    {
        // Haal de PlayerInput-component op en configureer de inputacties.
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Player" + PlayerNumber + "Movement"]; // Haal de bewegingsactie op, specifiek voor deze speler.
        _jumpAction = _playerInput.actions["Player" + PlayerNumber + "Jump"]; // Haal de springactie op, specifiek voor deze speler.

        // Abonneer op het jump-event om de Jump-methode aan te roepen wanneer de speler springt.
        _jumpAction.performed += Jump;

        // Haal de Rigidbody-component op om fysica toe te passen op het object.
        _rigidBody = GetComponent<Rigidbody>();

        // Zorg ervoor dat het spelerobject niet wordt vernietigd bij een scene-overgang.
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Lees de input van de speler voor beweging.
        Vector2 direction = _moveAction.ReadValue<Vector2>();

        // Beweeg de speler horizontaal op basis van de input en de snelheid.
        transform.position += new Vector3(direction.x * RunSpeed * Time.deltaTime, 0, 0);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        // Controleer of de speler op de grond staat voordat deze kan springen.
        if (_isGrounded)
        {
            // Voeg een verticale kracht toe om de speler te laten springen.
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, JumpForce, _rigidBody.velocity.z);

            // Markeer dat de speler niet langer op de grond is.
            _isGrounded = false;

            // Debug-log voor wanneer de speler springt.
            Debug.Log($"Player {PlayerNumber} jumped!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Controleer of de speler de grond raakt.
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true; // Markeer de speler als "op de grond".
        }

        // Controleer of de speler geraakt wordt door een projectiel.
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Death(); // Roep de Death-methode aan
        }
    }

    public void Death()
    {
        // Vernietig het spelerobject 
        Destroy(gameObject);
        Debug.Log($"Player {PlayerNumber} died!");
    }

    public void AssignGunner()
    {
        // Wijs de "gunner"-rol toe aan de speler.
        IsGunner = true;

        // Deactiveer het spelerobject
        gameObject.SetActive(false);

        // Debug-log voor wanneer de speler de "gunner"-rol krijgt.
        Debug.Log($"Player {PlayerNumber} is now the gunner!");
    }
}


