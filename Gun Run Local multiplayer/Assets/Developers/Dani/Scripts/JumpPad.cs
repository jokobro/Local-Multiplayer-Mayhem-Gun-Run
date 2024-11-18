using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField][Range(0, 10)] private int launchForce;
    [Tooltip("Multiply the x velocity of the player")][SerializeField][Range(0, 1)] private float reduceXSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerState>() != null)
        {
            Rigidbody _playerRB = other.gameObject.GetComponent<Rigidbody>();

            _playerRB.velocity = new Vector3(_playerRB.velocity.x * reduceXSpeed, _playerRB.velocity.y, _playerRB.velocity.z);
            _playerRB.velocity += new Vector3(0, 10, 0);
        }
    }
}
