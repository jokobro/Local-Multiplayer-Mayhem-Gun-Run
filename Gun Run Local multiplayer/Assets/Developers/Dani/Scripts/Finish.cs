using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.GetComponent<PlayerState>() != null) && other.gameObject.GetComponent<PlayerState>().state == PlayerStates.alive)
        {
            Debug.Log("Runners won");
        }
    }
}
