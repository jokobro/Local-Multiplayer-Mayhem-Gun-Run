using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBall : MonoBehaviour
{
    public bool isActivated;

    private void OnCollisionEnter(Collision other)
    {
        isActivated = true;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(10f);
    }
}
