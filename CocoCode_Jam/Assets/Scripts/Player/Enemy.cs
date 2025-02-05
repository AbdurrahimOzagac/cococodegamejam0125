using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1.2f;  // Saldýrý için gerekli mesafe
    public float attackDistance = 1.5f; // Raycast mesafesi
    public float attackCooldown = 2f;  // Saldýrý tekrar süresi

    public int maxHealth = 30; // Düþmanýn maksimum caný
    private int currentHealth; // Güncel can deðeri


    private Rigidbody rb;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();

        currentHealth = maxHealth; // Caný baþlangýçta maksimum yap
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            StartCoroutine(Attack());
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        //animator.SetBool("isWalking", true);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        rb.velocity = Vector3.zero; // Düþmaný durdur

        //animator.SetTrigger("Attack"); // Saldýrý animasyonunu tetikle

        yield return new WaitForSeconds(0.4f); // Yumruk çýkana kadar bekle

        if (PlayerStillInRange()) // Oyuncu hâlâ menzildeyse vur
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    bool PlayerStillInRange()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackDistance))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Caný azalt
        Debug.Log("Enemy hit! Current HP: " + currentHealth); // Konsola yazdýr (isteðe baðlý)

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // Düþmaný yok et
    }

}
