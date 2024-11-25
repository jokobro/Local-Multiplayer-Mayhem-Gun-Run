using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : Trap
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private int projectilesPerVolley;
    [SerializeField] private int volleyAmount;
    [SerializeField] private float timeBetweenProjectiles;
    [SerializeField] private float timeBetweenVolleys;
    private int onProjectile;
    private int onVolley;

    public override void ActivateTrap()
    {
        StartCoroutine(Volley());
    }

    IEnumerator Volley()
    {
        Instantiate(projectile, transform.position + (transform.forward * 1), Quaternion.identity);
        onProjectile++;
        yield return new WaitForSeconds(timeBetweenProjectiles);
        if (onProjectile >= projectilesPerVolley)
        {
            StartCoroutine(WaitForVolley());
        }
        else
        {
            StartCoroutine(Volley());
        }
    }

    IEnumerator WaitForVolley()
    {
        yield return new WaitForSeconds(timeBetweenVolleys);
        StartCoroutine(Volley());
    }
}
