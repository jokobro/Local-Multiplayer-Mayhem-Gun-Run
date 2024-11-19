using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : Trap
{

    [SerializeField][Range(1, 5)] private float fuseTime;
    [SerializeField][Range(1, 50)] private float explosionRadius;
    public override void ActivateTrap()
    {
        activated = true;
        StartCoroutine(fuse());
    }

    IEnumerator fuse()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    private void Explode()
    {
        Debug.Log(gameObject.name + " exploded");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if ((hitColliders[i].gameObject.GetComponent<PlayerState>() != null)
            && hitColliders[i].gameObject.GetComponent<PlayerState>().state == PlayerStates.alive)
            {
                hitColliders[i].gameObject.GetComponent<PlayerState>().Damage();
            }

            if ((hitColliders[i].gameObject.GetComponent<ExplodingBarrel>() != null)
            && !hitColliders[i].gameObject.GetComponent<ExplodingBarrel>().activated)
            {
                hitColliders[i].gameObject.GetComponent<ExplodingBarrel>().ActivateTrap();
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
