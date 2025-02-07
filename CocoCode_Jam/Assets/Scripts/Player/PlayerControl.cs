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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        // Kamera yönüne göre hareket vektörü hesapla
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Y bileşenini sıfırla (eğimli bakışlardan etkilenmemesi için)
        forward.y = 0;
        right.y = 0;

        // Hareket yönünü belirle (kameraya göre)
        Vector3 moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

        // Hareketi uygula
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);

        // Space tuşu ile zıplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
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
