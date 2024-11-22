using System.Collections;
using UnityEngine;

public class JumpPickup : MonoBehaviour
{
    private PlayerController _player;
    private void Start()
    {
        FindObjectOfType<PlayerController>().JumpForce = 7.5f;
        StartCoroutine(resetJumpForce());
    }

    private IEnumerator resetJumpForce()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<PlayerController>().JumpForce = 5f;
        Debug.Log("jump force gereset");
        Destroy(gameObject);
    }
}
