using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
   [SerializeField] private GameObject _winScreenRunners;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.GetComponent<PlayerState>() != null) && other.gameObject.GetComponent<PlayerState>().state == PlayerStates.alive)
        {
            Debug.Log("Runners won");
            _winScreenRunners.SetActive(true);
            
            //nog implementeren welke player won
        }
    }
}
