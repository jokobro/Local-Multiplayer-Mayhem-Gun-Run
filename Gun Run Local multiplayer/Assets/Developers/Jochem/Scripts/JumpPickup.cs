using System.Collections;
using UnityEngine;

public class JumpPickup : MonoBehaviour
{
    private void Start()
    {
        GetComponent<PlayerController>().JumpForce = 7.5f;
        StartCoroutine(resetJumpForce());
    }

    private IEnumerator resetJumpForce()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<PlayerController>().JumpForce = 5f;
        Debug.Log("jump force gereset");
        Destroy(this);
    }
}
