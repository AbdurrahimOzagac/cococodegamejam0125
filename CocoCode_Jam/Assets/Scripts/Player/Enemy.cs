using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float attackRange = 1.2f;  // Sald�r� i�in gerekli mesafe
    public float attackDistance = 1.5f; // Raycast mesafesi
    public float attackCooldown = 2f;  // Sald�r� tekrar s�resi

    public int maxHealth = 30; // D��man�n maksimum can�
    private int currentHealth; // G�ncel can de�eri

    private Rigidbody rb;
    private Animator animator;
    private bool isAttacking = false;
    private int attackIndex = -1; // Ba�lang��ta -1

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
        animator.SetInteger("AttackIndex", -1); // Sald�rmad���nda -1 yap
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        rb.velocity = Vector3.zero; // D��man� durdur
        animator.SetBool("isWalking", false);

        // Sald�r� animasyonu i�in index de�i�tir
        attackIndex = (attackIndex == 0) ? 1 : 0;
        animator.SetInteger("AttackIndex", attackIndex);
        animator.SetTrigger(attackIndex == 0 ? "Attack01" : "Attack02");

        yield return new WaitForSeconds(0.4f); // Yumruk ��kana kadar bekle

        if (PlayerStillInRange()) // Oyuncu h�l� menzildeyse vur
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        attackIndex = -1; // Sald�r� bitti�inde -1 yap
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
        isAttacking = true;
        Destroy(gameObject, 2f);
    }
}
