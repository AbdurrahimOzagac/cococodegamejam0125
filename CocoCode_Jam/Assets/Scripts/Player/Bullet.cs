using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f; // Mermi hýzý
    public float lifetime = 5f; // Süre sonunda yok olma
    public int damage = 15; // Merminin verdiði hasar

    void Start()
    {
        Destroy(gameObject, lifetime); // Belirli süre sonra yok ol
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime); // Düz bir çizgide ilerle
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Eðer çarpýlan nesne düþman ise
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Enemy bileþenini al
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Hasar ver
            }

            Destroy(gameObject); // Mermiyi yok et
        }
    }
}
