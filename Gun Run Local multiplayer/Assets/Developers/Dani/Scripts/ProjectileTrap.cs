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

    [Range(0, 10)][SerializeField] private float projectileMoveSpeed;
    [Tooltip("How much the projectile should levitate")][Range(0, 10)][SerializeField] private float projectileFloatMult;

    private void Awake()
    {
        ActivateTrap();
    }

    public override void ActivateTrap()
    {
        StartCoroutine(Volley());
    }

    IEnumerator Volley()
    {
        Projectile _projectile = Instantiate(projectile, transform.position + (transform.forward * 1), Quaternion.identity).GetComponent<Projectile>();
        _projectile.floatMult = projectileFloatMult;
        _projectile.gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * projectileMoveSpeed + Vector3.up * 5;
        onProjectile++;
        yield return new WaitForSeconds(timeBetweenProjectiles);
        if (onProjectile >= projectilesPerVolley)
        {
            onProjectile = 0;
            onVolley++;
            if (onVolley < volleyAmount)
            {
                StartCoroutine(WaitForVolley());
            }
        }
        else if (onVolley < volleyAmount)
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
