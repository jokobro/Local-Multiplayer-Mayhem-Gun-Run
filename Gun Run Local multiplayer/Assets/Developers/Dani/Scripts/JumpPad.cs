using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private Animator _jumpPadAnim;

    [SerializeField][Range(0, 10)] private int _launchForce;
    [Tooltip("Multiply the x velocity of the player")][SerializeField][Range(0, 1)] private float _reduceXSpeed;

    private void Awake()
    {
        _jumpPadAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerState>() != null)
        {
            Rigidbody _playerRB = other.gameObject.GetComponent<Rigidbody>();
            _jumpPadAnim.Play("Launch", 0, 0);

            _playerRB.velocity = new Vector3(_playerRB.velocity.x * _reduceXSpeed, _playerRB.velocity.y, _playerRB.velocity.z);
            _playerRB.velocity += new Vector3(0, 10, 0);
        }
    }
}
