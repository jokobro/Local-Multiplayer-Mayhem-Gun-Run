using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : Trap
{
    [SerializeField] private GameObject _projectileGameObject;

    [SerializeField] private int _projectilesPerVolley;
    [SerializeField] private int _volleyAmount;
    [SerializeField] private float _timeBetweenProjectiles;
    [SerializeField] private float _timeBetweenVolleys;
    private int _onProjectile;
    private int _onVolley;

    [Range(0, 10)][SerializeField] private float _projectileMoveSpeed;
    [Tooltip("How much the projectile should levitate")][Range(0, 10)][SerializeField] private float _projectileFloatMult;

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
        Projectile _projectile = Instantiate(_projectileGameObject, transform.position + (transform.forward * 1), Quaternion.identity).GetComponent<Projectile>();
        _projectile.floatMult = _projectileFloatMult;
        _projectile.gameObject.GetComponent<Rigidbody>().velocity = Vector3.forward * _projectileMoveSpeed + Vector3.up * 5;
        _onProjectile++;
        yield return new WaitForSeconds(_timeBetweenProjectiles);
        if (_onProjectile >= _projectilesPerVolley)
        {
            _onProjectile = 0;
            _onVolley++;
            if (_onVolley < _volleyAmount)
            {
                StartCoroutine(WaitForVolley());
            }
        }
        else if (_onVolley < _volleyAmount)
        {
            StartCoroutine(Volley());
        }
    }

    IEnumerator WaitForVolley()
    {
        yield return new WaitForSeconds(_timeBetweenVolleys);
        StartCoroutine(Volley());
    }
}
