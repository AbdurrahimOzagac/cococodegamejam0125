using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player; // Player'ýn Transform'u
    public float moveSpeed = 3f; // Düþmanýn hareket hýzý
    private Rigidbody rb; // Düþmanýn Rigidbody komponenti

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody'yi al
    }

    void Update()
    {
        if (player == null) return;

        // Player'a doðru yönel
        Vector3 direction = (player.position - transform.position).normalized;

        // Hareket et (y eksenini sabit tut)
        Vector3 velocity = new Vector3(direction.x, 0f, direction.z) * moveSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        // Oyuncuya doðru bakmasýný saðla
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
