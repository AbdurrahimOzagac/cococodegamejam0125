using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Player'�n Transform'u
    public float moveSpeed = 3f; // D��man�n hareket h�z�
    private Rigidbody rb; // D��man�n Rigidbody komponenti

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody'yi al
    }

    void Update()
    {
        if (player == null) return;

        // Player'a do�ru y�nel
        Vector3 direction = (player.position - transform.position).normalized;

        // Hareket et (y eksenini sabit tut)
        Vector3 velocity = new Vector3(direction.x, 0f, direction.z) * moveSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        // Oyuncuya do�ru bakmas�n� sa�la
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
