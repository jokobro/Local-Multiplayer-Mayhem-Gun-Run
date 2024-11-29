using UnityEngine;
using UnityEngine.InputSystem;
public class Gunner : MonoBehaviour
{
   /* [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnpoint;
    private float _nextfire;
    private float _bulletSpeed = 6f;
    public float FireRate = 3.0f;
    public bool BlockFireRatePickUP = false;


    public void shoot(InputAction.CallbackContext context)
    {
        if (BlockFireRatePickUP == true)
        {
            Debug.Log("Cant fire");
        }
        else if (BlockFireRatePickUP == false && Time.time > _nextfire)
        {
            if (context.performed)
            {
                _nextfire = Time.time + FireRate;
                var bullet = Instantiate(_bulletPrefab, _bulletSpawnpoint);
                bullet.transform.parent = null;
                bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnpoint.forward * _bulletSpeed;
            }
        }
    }*/
}
