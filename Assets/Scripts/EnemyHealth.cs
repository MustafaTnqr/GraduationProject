using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20; // Maksimum can
    private int currentHealth;  // �u anki can
    private Animator animator;
    private bool isDead = false; // �l�m durumu kontrol�

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // E�er zaten �ld�yse i�lem yapma

        // Can� azalt
        currentHealth -= damage;

        // �l�m kontrol�
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true; // �l�m durumu i�aretle
        animator.SetBool("IsDead", true); // �l�m animasyonunu tetikle

        // Hareketi durdurmak i�in EnemyMovement scriptini �a��r
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.StopMovement();
        }

        // �l�m animasyonu bittikten sonra objeyi sahneden kald�r
        Destroy(gameObject, 1.5f); // 1.5 saniye sonra yok et
    }
}
