using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool isActivated;

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GunnerBall>() != null && !isActivated)
        {
            other.gameObject.GetComponent<GunnerBall>().isActivated = true;
            ActivateTrap();
        }
    }

    public virtual void ActivateTrap()
    {
        isActivated = true;
    }
}
