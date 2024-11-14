using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;


    


    public float RunSpeed = 3f;
    public float JumpHeight = 5f;


    


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

    }

    private void Update()
    {


    }

}
