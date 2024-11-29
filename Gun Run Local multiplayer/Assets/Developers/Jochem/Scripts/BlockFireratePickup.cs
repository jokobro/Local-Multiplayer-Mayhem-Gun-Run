using System.Collections;
using UnityEngine;

public class BlockFireratePickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            FindObjectOfType<PlayerController>().BlockFireRatePickUP = true;
            StartCoroutine(ResetFireRate());
        }
    }

    private IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(15);
        FindObjectOfType<PlayerController>().BlockFireRatePickUP = false;
        Debug.Log("fire rate gereset");
        Destroy(gameObject);
    }
}
