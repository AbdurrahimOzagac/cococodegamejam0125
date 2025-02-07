using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShoulder : MonoBehaviour
{
    [SerializeField] private Transform target;   // Takip edilecek oyuncu (karakterin Transform'u)
    [SerializeField] private Vector3 offset = new Vector3(2f, 3f, -4f); // Omuz �st� pozisyonu
    [SerializeField] private float sensitivity = 3f; // Mouse hassasiyeti
    [SerializeField] private float minY = -30f, maxY = 60f; // Yukar�/a�a�� bak�� limitleri
    [SerializeField] private float collisionOffset = 0.5f; // �arp��ma sonras� uzakl�k

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �mleci ekranda kilitle
    }

    void LateUpdate()
    {
        if (!target) return;

        // Mouse giri�lerini al
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // X ekseninde karakteri d�nd�r
        rotationX += mouseX;

        // Y ekseninde kameray� s�n�rlar i�inde d�nd�r
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // Kameray� hedefin etraf�nda d�nd�r
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Kameran�n hedefe do�ru raycast atmas� ve �arp��ma olup olmad���n� kontrol etmesi
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredPosition - target.position, out hit, offset.magnitude))
        {
            // �arp��ma varsa, kameray� �arp��ma noktas�na yak�n tut ve biraz daha geri �ek
            transform.position = hit.point + hit.normal * collisionOffset;
        }
        else
        {
            // �arp��ma yoksa, normal pozisyona geri d�n
            transform.position = desiredPosition;
        }

        // Kamera hedefe odaklanmaya devam et
        transform.LookAt(target.position + Vector3.up * 1.5f); // Karakterin �st�ne odaklan
    }
}
