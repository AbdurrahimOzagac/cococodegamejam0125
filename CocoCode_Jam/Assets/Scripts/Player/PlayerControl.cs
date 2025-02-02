using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;  // Hareket hızı
    public float jumpForce = 5f;  // Zıplama gücü
    private Rigidbody rb;
    private bool isGrounded = true;
    public Transform cameraTransform; // Kamerayı referans alacağız

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Kamera yönüne göre hareket vektörü hesapla
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y bileşenini sıfırla (eğimli bakışlardan etkilenmemesi için)
        forward.y = 0;
        right.y = 0;

        // Hareket yönünü belirle (kameraya göre)
        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // Hareketi uygula
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Space tuşu ile zıplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Eğer oyuncu hareket ediyorsa yönünü güncelle
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Jumpable"))
        {
            isGrounded = true;
        }
    }
}
