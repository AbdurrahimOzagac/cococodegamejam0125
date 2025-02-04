using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab; // F�rlat�lacak top
    public Transform firePoint; // Topun ��k�� noktas�
    public float shootForce = 10f; // F�rlatma kuvveti
    public float gravityScale = 0.5f; // Yer�ekimi etkisi

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol t�k ile ate�leme
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        // Topu olu�tur
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Rigidbody bile�enini al
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Karakterin ileri y�n�nde kuvvet uygula
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);

            // Yer�ekimini azalt
            rb.useGravity = true;
            rb.mass *= gravityScale;
        }
    }
}
