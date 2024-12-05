using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float _swingTime;
    public bool isGuarenteedHit = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void ActivateAxe()
    {
        _rb.isKinematic = false;
        StartCoroutine(LoseHit());
    }

    public IEnumerator LoseHit()
    {
        yield return new WaitForSeconds(_swingTime);
        isGuarenteedHit = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (((other.gameObject.GetComponent<PlayerState>() != null) && other.gameObject.GetComponent<PlayerState>().state == PlayerStates.alive) && (isGuarenteedHit || _rb.velocity.magnitude >= 8))
        {
            other.gameObject.GetComponent<PlayerState>().Damage();
        }
    }
}
