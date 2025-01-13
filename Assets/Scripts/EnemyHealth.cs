using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20; // Maksimum can
    private int currentHealth; // Þu anki can
    private Animator animator;
    private bool isDead = false; // Ölüm durumu kontrolü

    void Start()
    {
        currentHealth = maxHealth; // Caný maksimum yap
        animator = GetComponent<Animator>();

    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Eðer zaten öldüyse iþlem yapma

        currentHealth -= damage; // Caný azalt
        

        // Eðer can sýfýrýn altýna düþtüyse ölümü çaðýr
        if (currentHealth <= 0)
        {
            Die();
        }
    }
        

    void Die()
    {
        if (isDead) return; // Eðer zaten öldüyse iþlem yapma

        isDead = true; // Ölüm durumunu iþaretle
        

        // Ölüm animasyonu tetikle
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }

        // Hareketi durdur
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.enabled = false;
        }

        // Collider'ý kapatarak düþmanýn çarpmalara neden olmasýný engelle
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // Ölüm animasyonu bitince objeyi sahneden kaldýr
        Destroy(gameObject, 1f);
    }
}
