using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool activated;

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GunnerBall>() != null && !activated)
        {
            other.gameObject.GetComponent<GunnerBall>().activated = true;
            ActivateTrap();
        }
    }

    public virtual void ActivateTrap()
    {

    }
}
