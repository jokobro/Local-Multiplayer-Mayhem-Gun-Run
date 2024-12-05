using UnityEngine;
using UnityEngine.InputSystem;
public class Gunner : MonoBehaviour
{
    public Camera MainCamera; // Camera voor targeting
    public GameObject BulletPrefab; // Prefab van de kogel
    public Transform BulletSpawnPoint; // Spawnlocatie van de kogel
    public float BulletSpeed = 10f; // Kracht waarmee de kogel wordt geschoten
    public float FireRate = 0.5f; // Vuursnelheid
    private float _nextFireTime;

    private void Update()
    {
        Aim();
        if (Input.GetButtonDown("Fire1") && Time.time >= _nextFireTime)
        {
            Shoot();
        }
    }

    // Richt het wapen naar de crosshair/target
    private void Aim()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point; // Verplaats de gunner/crosshair naar de hitlocatie
        }
    }

    // Schiet een kogel
    public void Shoot()
    {
        _nextFireTime = Time.time + FireRate;

        // Maak een kogel aan en voeg kracht toe
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Richt de kogel richting het doel met een boog
        Vector3 direction = (transform.forward + transform.forward * 0.5f).normalized; // Voeg een opwaartse component toe
        bulletRb.AddForce(direction * BulletSpeed, ForceMode.Impulse); // Gebruik een impuls voor een boogje

        Debug.Log("Gunner heeft geschoten!");
    }
}
