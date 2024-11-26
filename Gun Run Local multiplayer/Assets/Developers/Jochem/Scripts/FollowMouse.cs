using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 Position;
    private float _speed = 1f;



    private void Update()
    {
        Position = Input.mousePosition;
        Position.z = _speed;
        transform.position = Camera.main.ScreenToWorldPoint(Position);
    }


}
