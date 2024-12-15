using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target; // Takip edilecek hedef
    public float speed = 2f; // Düþman hýzý
    public float attackRange = 1.5f; // Saldýrý mesafesi
    public float attackCooldown = 1.5f; // Saldýrý bekleme süresi
    public int attackDamage = 10; // Saldýrý hasarý
    private bool isAttacking = false; // Saldýrý durumu
    private Animator animator;
    private bool isDead = false; // Ölüm durumu kontrolü

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return; // Düþman öldüyse hareketi durdur
        if (PlayerHealth.isPlayerDead)
        {
            StopMovement(); // Oyuncu öldüðünde düþmanlarý durdur
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
        // Hedefe doðru yön hesapla
        Vector3 direction = (target.position - transform.position).normalized;

        // Hareket
        transform.position += direction * speed * Time.deltaTime;

        // Animasyon parametrelerini ayarla
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetBool("IsAttacking", false); // Saldýrý durumu kapalý
    }

    void Attack()
    {
        isAttacking = true;

        // Oyuncuya doðru yönü ayarla
        Vector3 direction = (target.position - transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetBool("IsAttacking", true); // Saldýrý animasyonunu baþlat

        // Oyuncuya hasar ver
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        // Saldýrý animasyonunun bitiþi için bekleme süresi
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("IsAttacking", false); // Saldýrý durumu sýfýrla
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetBool("IsDead", true); // Ölüm animasyonunu baþlat
        StopMovement(); // Hareketi durdur
        Destroy(gameObject, 2f); // Ölüm animasyonu bittikten sonra objeyi sil
    }

    // Hareketi durduran metod
    public void StopMovement()
    {
        isAttacking = true;
        speed = 0; // Hýzý sýfýrla
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetBool("IsAttacking", false);
    }
}
