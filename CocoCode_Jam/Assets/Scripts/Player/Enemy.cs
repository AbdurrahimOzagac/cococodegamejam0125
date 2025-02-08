using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;  // Sald�r� i�in gerekli mesafe
    public float attackDistance = 1.5f; // Raycast mesafesi
    public float attackCooldown = 2f;  // Sald�r� tekrar s�resi

    public int maxHealth = 30; // D��man�n maksimum can�
    private int currentHealth; // G�ncel can de�eri

    private Rigidbody rb;
    private Animator animator;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth; // Can� ba�lang��ta maksimum yap
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
        if (isAttacking) return; // Sald�r� s�ras�nda hareket etme

        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        animator.SetBool("isWalking", true);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        rb.velocity = Vector3.zero; // D��man� durdur
        animator.SetBool("isWalking", false);

        // Sald�r� animasyonu i�in index de�i�tir
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.4f); // Yumruk ��kana kadar bekle

        if (PlayerStillInRange()) // Oyuncu h�l� menzildeyse vur
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }

        yield return new WaitForSeconds(attackCooldown + 0.5f); // Attack delay ekle

        isAttacking = false;
    }


    bool PlayerStillInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy hit! Current HP: " + currentHealth);

        animator.SetTrigger("GetHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    

    void Die()
    {
        Debug.Log("Enemy died!");
        animator.SetTrigger("Die");
        rb.velocity = Vector3.zero;
        Destroy(gameObject, 2f);
    }
}
