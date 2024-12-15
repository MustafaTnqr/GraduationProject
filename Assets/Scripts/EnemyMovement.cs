using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target; // Takip edilecek hedef
    public float speed = 2f; // D��man h�z�
    public float attackRange = 1.5f; // Sald�r� mesafesi
    public float attackCooldown = 1.5f; // Sald�r� bekleme s�resi
    public int attackDamage = 10; // Sald�r� hasar�
    private bool isAttacking = false; // Sald�r� durumu
    private Animator animator;
    private bool isDead = false; // �l�m durumu kontrol�

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return; // D��man �ld�yse hareketi durdur
        if (PlayerHealth.isPlayerDead)
        {
            StopMovement(); // Oyuncu �ld���nde d��manlar� durdur
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange && !isAttacking)
        {
            Attack();
        }
        else if (!isAttacking)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        // Hedefe do�ru y�n hesapla
        Vector3 direction = (target.position - transform.position).normalized;

        // Hareket
        transform.position += direction * speed * Time.deltaTime;

        // Animasyon parametrelerini ayarla
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetBool("IsAttacking", false); // Sald�r� durumu kapal�
    }

    void Attack()
    {
        isAttacking = true;

        // Oyuncuya do�ru y�n� ayarla
        Vector3 direction = (target.position - transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetBool("IsAttacking", true); // Sald�r� animasyonunu ba�lat

        // Oyuncuya hasar ver
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        // Sald�r� animasyonunun biti�i i�in bekleme s�resi
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("IsAttacking", false); // Sald�r� durumu s�f�rla
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("IsDead", true); // �l�m animasyonunu ba�lat
        StopMovement(); // Hareketi durdur
        Destroy(gameObject, 2f); // �l�m animasyonu bittikten sonra objeyi sil
    }

    // Hareketi durduran metod
    public void StopMovement()
    {
        isAttacking = true;
        speed = 0; // H�z� s�f�rla
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetBool("IsAttacking", false);
    }
}
