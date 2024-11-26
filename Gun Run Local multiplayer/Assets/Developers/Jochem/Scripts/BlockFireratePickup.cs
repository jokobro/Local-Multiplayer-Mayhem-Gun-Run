using System.Collections;
using UnityEngine;

public class BlockFireratePickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            FindObjectOfType<Gunner>().BlockFireRatePickUP = true;
            StartCoroutine(ResetFireRate());
        }
    }

    private IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(15);
        FindObjectOfType<Gunner>().BlockFireRatePickUP = false;
        Debug.Log("fire rate gereset");
        Destroy(gameObject);
    }
}
