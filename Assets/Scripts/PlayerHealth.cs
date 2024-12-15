using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static bool isPlayerDead = false;
    public int maxHealth = 100;
    private float currentHealth;
    private Animator animator;
    private bool isDead = false;

    public Slider healthBar;
    public float healDelay = 5f;
    public float healDuration = 7f;

    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        isPlayerDead = false;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        if (!isDead && Time.time - lastDamageTime >= healDelay && currentHealth < maxHealth)
        {
            HealOverTime();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        lastDamageTime = Time.time;

        UpdateHealthBar(false); // Lerp kapalý güncelleme

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void HealOverTime()
    {
        float healAmount = (maxHealth / healDuration) * Time.deltaTime;
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar(true); // Lerp açýk güncelleme
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        isPlayerDead = true;
        animator.SetBool("IsDead", true);

        if (GetComponent<CharacterMovement>() != null)
            GetComponent<CharacterMovement>().enabled = false;

        if (GetComponent<CharacterShooting>() != null)
            GetComponent<CharacterShooting>().enabled = false;

        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void UpdateHealthBar(bool isHealing)
    {
        if (healthBar != null)
        {
            if (isHealing)
            {
                // Can artarken yumuþak geçiþ
                healthBar.value = Mathf.Lerp(healthBar.value, currentHealth, Time.deltaTime * 5f);
            }
            else
            {
                // Can azalýrken anýnda güncelle
                healthBar.value = currentHealth;
            }
        }
    }
}
