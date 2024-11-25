using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : Trap
{

    [SerializeField][Range(1, 5)] private float fuseTime;
    [SerializeField][Range(1, 50)] private float explosionRadius;
    [SerializeField][Range(0, 2500)] private int explosionForce;
    [SerializeField] private LayerMask dontHit;
    
    public override void ActivateTrap()
    {
        StartCoroutine(fuse());
    }

    IEnumerator fuse()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    private void Explode()
    {
        gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject hitGameObject = hitColliders[i].gameObject;
            Vector3 hitDirection = hitGameObject.transform.position - transform.position;
            float hitDistance = Vector3.Distance(transform.position, hitGameObject.transform.position) / explosionRadius;
            Debug.Log(hitGameObject.name + " " + hitDistance);
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, hitDirection, out hit,
            (hitGameObject.transform.position - transform.position).magnitude, ~dontHit))
            {


                {


                    // knock and move objects away if they have a rigidbody
                    if ((hitGameObject.gameObject.GetComponent<Rigidbody>() != null)
                    && !hitGameObject.gameObject.GetComponent<Rigidbody>().isKinematic)
                    {
                        hitGameObject.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection.normalized * explosionForce / (hitDistance + 0.5f), ForceMode.Force);

                    }

                    // damage any player within it's radius
                    if (hitGameObject.gameObject.GetComponent<PlayerState>() != null)
                    {
                        hitGameObject.gameObject.GetComponent<PlayerState>().Damage();
                    }

                    // activate any other exploding barrel hit
                    if ((hitGameObject.gameObject.GetComponent<ExplodingBarrel>() != null)
                    && !hitGameObject.gameObject.GetComponent<ExplodingBarrel>().activated)
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
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
