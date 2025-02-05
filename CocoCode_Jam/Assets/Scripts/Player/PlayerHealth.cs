using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }  // D��ar�dan okunabilir ama sadece fonksiyonlarla de�i�tirilebilir

    [SerializeField] private Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthBar)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player �ld�!");
        // �l�m ekran�, sahne resetleme veya oyuncuyu devre d��� b�rakma se�enekleri
    }
}
