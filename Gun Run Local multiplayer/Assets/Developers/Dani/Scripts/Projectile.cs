using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public GameObject _parent;

    [HideInInspector] public float floatMult;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(Vector3.up * floatMult / 10);
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.GetComponent<PlayerState>() != null) && other.gameObject.GetComponent<PlayerState>().state == PlayerStates.alive)
        {
            other.gameObject.GetComponent<PlayerState>().Damage();
        }
        else if (other.gameObject != _parent)
        {
            Destroy(gameObject);
        }
    }
}
