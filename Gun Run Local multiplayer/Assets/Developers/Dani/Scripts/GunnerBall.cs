using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBall : MonoBehaviour
{
    private Rigidbody _rb;
    public bool activated;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity += -Vector3.forward * 10;
        _rb.velocity += Vector3.up * 5;
    }

    // Update is called once per frame
    void Update()
    {

    }
}