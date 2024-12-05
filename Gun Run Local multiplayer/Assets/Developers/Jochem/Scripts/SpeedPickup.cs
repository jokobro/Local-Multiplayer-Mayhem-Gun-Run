using System.Collections;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private PlayerController _player;

    private void Start()
    {
        activateSpeedPickup();
    }

    /* private void OnCollisionEnter(Collision collision)
     {
         if(collision.gameObject.tag == "player")
         {
             FindObjectOfType<PlayerController>().RunSpeed = 4.5f;
             StartCoroutine(resetRunSpeedBack());
         }
     }*/

    private void activateSpeedPickup()
    {
        GetComponent<PlayerController>().RunSpeed = 4.5f;
        StartCoroutine(resetRunSpeedBack());
    }

    private IEnumerator resetRunSpeedBack()
    {
        yield return new WaitForSeconds(12f);
        GetComponent<PlayerController>().RunSpeed = 3f;
        Debug.Log("Runspeed is terug naar normaal");
        Destroy(this);
    }
}
