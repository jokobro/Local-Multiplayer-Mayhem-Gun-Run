using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : Trap
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _fuse;
    [SerializeField] private GameObject _explosionIndicator;
    [SerializeField][Range(1, 5)] private float _fuseTime;
    [SerializeField][Range(1, 50)] private float _explosionRadius;
    [SerializeField][Range(0, 2500)] private int _explosionForce;
    [SerializeField] private LayerMask _dontHit;

    public override void ActivateTrap()
    {
        base.ActivateTrap();
        StartCoroutine(fuse());
    }

    IEnumerator fuse()
    {
        Instantiate(_fuse, transform.position + _fuse.transform.position, Quaternion.identity, gameObject.transform);
        GameObject _indicator = Instantiate(_explosionIndicator, transform.position, Quaternion.identity, gameObject.transform);
        _indicator.transform.localScale = new Vector3(2, _explosionRadius * 2, _explosionRadius * 2);
        yield return new WaitForSeconds(_fuseTime);
        Explode();
    }

    private void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject hitGameObject = hitColliders[i].gameObject;
            Vector3 hitDirection = hitGameObject.transform.position - transform.position;
            float hitDistance = Vector3.Distance(transform.position, hitGameObject.transform.position) / _explosionRadius;
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, hitDirection, out hit,
            (hitGameObject.transform.position - transform.position).magnitude, ~_dontHit))
            {
                {
                    // knock and move objects away if they have a rigidbody
                    if ((hitGameObject.gameObject.GetComponent<Rigidbody>() != null)
                    && !hitGameObject.gameObject.GetComponent<Rigidbody>().isKinematic)
                    {
                        hitGameObject.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection.normalized * _explosionForce / (hitDistance + 0.5f), ForceMode.Force);

                    }

                    // damage any player within it's radius
                    if ((hitGameObject.GetComponent<PlayerState>() != null) && hitGameObject.GetComponent<PlayerState>().state == PlayerStates.alive)
                    {
                        hitGameObject.gameObject.GetComponent<PlayerState>().Damage();
                    }

                    // activate any other exploding barrel hit
                    if ((hitGameObject.gameObject.GetComponent<Trap>() != null)
                    && !hitGameObject.gameObject.GetComponent<Trap>().isActivated)
                    {
                        hitGameObject.gameObject.GetComponent<Trap>().ActivateTrap();
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position in the editor to see
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
