using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float swingTime;
    public bool guarenteedHit = true;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    public void ActivateAxe()
    {
        _rb.isKinematic = false;
        StartCoroutine(LoseHit());
    }

    public IEnumerator LoseHit()
    {
        Debug.Log("IEnum");
        yield return new WaitForSeconds(swingTime);
        guarenteedHit = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (((other.gameObject.GetComponent<PlayerState>() != null) && other.gameObject.GetComponent<PlayerState>().state == PlayerStates.alive) && (guarenteedHit || _rb.velocity.magnitude >= 8))
        {
            other.gameObject.GetComponent<PlayerState>().Damage();
        }
    }
}
