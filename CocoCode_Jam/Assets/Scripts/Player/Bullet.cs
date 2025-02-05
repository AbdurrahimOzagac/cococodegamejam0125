using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f; // Mermi h�z�
    public float lifetime = 5f; // S�re sonunda yok olma
    public int damage = 15; // Merminin verdi�i hasar

    void Start()
    {
        Destroy(gameObject, lifetime); // Belirli s�re sonra yok ol
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // D�z bir �izgide ilerle
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // E�er �arp�lan nesne d��man ise
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Enemy bile�enini al
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Hasar ver
            }

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
