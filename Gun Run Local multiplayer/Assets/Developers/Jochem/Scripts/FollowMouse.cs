using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Vector3 _position;
    private float _speed = 1f;

    private void Update()
    {
        _position = Input.mousePosition;
        _position.z = _speed;
        transform.position = Camera.main.ScreenToWorldPoint(_position);
    }
}
