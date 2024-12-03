using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : Trap
{
    [SerializeField][Range(1, 5)] private float _fuseTime;
    [SerializeField][Range(1, 50)] private float _explosionRadius;
    [SerializeField][Range(0, 2500)] private int _explosionForce;
    [SerializeField] private LayerMask _dontHit;

    public override void ActivateTrap()
    {
        StartCoroutine(fuse());
    }

    IEnumerator fuse()
    {
        yield return new WaitForSeconds(_fuseTime);
        Explode();
    }

    private void Explode()
    {
        gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject hitGameObject = hitColliders[i].gameObject;
            Vector3 hitDirection = hitGameObject.transform.position - transform.position;
            float hitDistance = Vector3.Distance(transform.position, hitGameObject.transform.position) / _explosionRadius;
            Debug.Log(hitGameObject.name + " " + hitDistance);
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
                    if (hitGameObject.gameObject.GetComponent<PlayerState>() != null)
                    {
                        hitGameObject.gameObject.GetComponent<PlayerState>().Damage();
                    }

                    // activate any other exploding barrel hit
                    if ((hitGameObject.gameObject.GetComponent<ExplodingBarrel>() != null)
                    && !hitGameObject.gameObject.GetComponent<ExplodingBarrel>().isActivated)
                    {
                        hitGameObject.gameObject.GetComponent<ExplodingBarrel>().ActivateTrap();
                    }
                }
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
