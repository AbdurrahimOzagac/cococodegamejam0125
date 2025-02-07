using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShoulder : MonoBehaviour
{
    [SerializeField] private Transform target;   // Takip edilecek oyuncu (karakterin Transform'u)
    [SerializeField] private Vector3 offset = new Vector3(2f, 3f, -4f); // Omuz üstü pozisyonu
    [SerializeField] private float sensitivity = 3f; // Mouse hassasiyeti
    [SerializeField] private float minY = -30f, maxY = 60f; // Yukarý/aþaðý bakýþ limitleri
    [SerializeField] private float collisionOffset = 0.5f; // Çarpýþma sonrasý uzaklýk

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ýmleci ekranda kilitle
    }

    void LateUpdate()
    {
        if (!target) return;

        // Mouse giriþlerini al
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // X ekseninde karakteri döndür
        rotationX += mouseX;

        // Y ekseninde kamerayý sýnýrlar içinde döndür
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        // Kamerayý hedefin etrafýnda döndür
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Kameranýn hedefe doðru raycast atmasý ve çarpýþma olup olmadýðýný kontrol etmesi
        RaycastHit hit;
        if (Physics.Raycast(target.position, desiredPosition - target.position, out hit, offset.magnitude))
        {
            // Çarpýþma varsa, kamerayý çarpýþma noktasýna yakýn tut ve biraz daha geri çek
            transform.position = hit.point + hit.normal * collisionOffset;
        }
        else
        {
            // Çarpýþma yoksa, normal pozisyona geri dön
            transform.position = desiredPosition;
        }

        // Kamera hedefe odaklanmaya devam et
        transform.LookAt(target.position + Vector3.up * 1.5f); // Karakterin üstüne odaklan
    }
}
