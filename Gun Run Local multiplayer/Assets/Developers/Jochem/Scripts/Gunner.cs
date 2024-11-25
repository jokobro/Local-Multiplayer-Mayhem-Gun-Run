using UnityEngine;
using UnityEngine.InputSystem;
public class Gunner : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnpoint;
    private float _nextfire;
    public float FireRate = 3.0f;



    private void Start()
    {

    }

    private void Update()
    {

    }


    public void shoot(InputAction.CallbackContext context)
    {
        if (Time.time > _nextfire)
        {
            if (context.performed)
            {
                _nextfire = Time.time + FireRate;
                var bullet = Instantiate(_bulletPrefab, _bulletSpawnpoint);
            }
        }
    }
}
