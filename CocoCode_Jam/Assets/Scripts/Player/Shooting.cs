using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefabý
    public Transform firePoint; // Merminin çýkýþ noktasý

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týk ile ateþ et
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
