using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20; // Maksimum can
    private int currentHealth;  // Þu anki can
    private Animator animator;
    private bool isDead = false; // Ölüm durumu kontrolü

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Eðer zaten öldüyse iþlem yapma

        // Caný azalt
        currentHealth -= damage;

        // Ölüm kontrolü
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true; // Ölüm durumu iþaretle
        animator.SetBool("IsDead", true); // Ölüm animasyonunu tetikle

        // Hareketi durdurmak için EnemyMovement scriptini çaðýr
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.StopMovement();
        }

        // Ölüm animasyonu bittikten sonra objeyi sahneden kaldýr
        Destroy(gameObject, 1.5f); // 1.5 saniye sonra yok et
    }
}
