using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : Trap
{
    [SerializeField] private GameObject _projectileGameObject;
    [SerializeField] private GameObject _explosion;

    [SerializeField] private int _projectilesPerVolley;
    [SerializeField] private int _volleyAmount;
    [SerializeField] private float _timeBetweenProjectiles;
    [SerializeField] private float _timeBetweenVolleys;
    private int _onProjectile;
    private int _onVolley;

    [Range(0, 30)][SerializeField] private float _projectileMoveSpeed;
    [Tooltip("How much the projectile should levitate")][Range(0, 10)][SerializeField] private float _projectileFloatMult;

    public override void ActivateTrap()
    {
        base.ActivateTrap();
        StartCoroutine(Volley());
    }

    IEnumerator Volley()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Projectile _projectile = Instantiate(_projectileGameObject, transform.position, Quaternion.identity).GetComponent<Projectile>();

        _projectile._parent = gameObject;
        _projectile.floatMult = _projectileFloatMult;
        _projectile.gameObject.GetComponent<Rigidbody>().velocity = -transform.forward * _projectileMoveSpeed + Vector3.up * 5;
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
