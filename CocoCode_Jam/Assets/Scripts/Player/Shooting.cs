using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Fýrlatýlacak top
    public Transform firePoint; // Topun çýkýþ noktasý
    public float shootForce = 10f; // Fýrlatma kuvveti
    public float gravityScale = 0.5f; // Yerçekimi etkisi

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol týk ile ateþleme
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        // Topu oluþtur
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Rigidbody bileþenini al
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Karakterin ileri yönünde kuvvet uygula
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);

            // Yerçekimini azalt
            rb.useGravity = true;
            rb.mass *= gravityScale;
        }
    }
}
