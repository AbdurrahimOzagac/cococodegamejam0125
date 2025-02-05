using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab�
    public Transform firePoint; // Merminin ��k�� noktas�

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�k ile ate� et
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
