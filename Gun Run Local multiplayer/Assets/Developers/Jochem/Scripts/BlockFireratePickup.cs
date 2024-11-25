using System.Collections;
using UnityEngine;

public class BlockFireratePickup : MonoBehaviour
{
    private Gunner _gunner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            StartCoroutine(ResetFireRate());
        }
    }

    private IEnumerator ResetFireRate()
    {
        FindObjectOfType<Gunner>().FireRate = 30f;
        yield return new WaitForSeconds(15);
        FindObjectOfType<Gunner>().FireRate = 3f;
        Debug.Log("fire rate gereset");
        Destroy(gameObject);
    }
}
