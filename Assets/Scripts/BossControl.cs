using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // Hareket hýzý
    public float attackRange = 10f; // Saldýrý mesafesi
    public float attackCooldown = 2f; // Saldýrýlar arasýndaki bekleme süresi

    private Transform player; // Oyuncunun Transform'u
    private Animator animator;
    private Vector3 originalScale; // Bossun orijinal scale deðeri
    private float lastAttackTime; // Son saldýrý zamaný
    private bool isDead = false; // Boss'un ölüm durumu kontrolü
    private Rigidbody2D rb; // Rigidbody referansý

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // Baþlangýç scale deðerini kaydet

        // Coroutine ile oyuncuyu bul
        StartCoroutine(FindPlayer());
    }

    private System.Collections.IEnumerator FindPlayer()
    {
        while (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Player bulundu!");
            }
            yield return null; // Bir sonraki frame'de tekrar dene
        }
    }

    private void Update()
    {
        if (isDead || player == null) return; // Eðer boss ölü veya oyuncu yoksa iþlem yapma

        // Oyuncu ölü ise bossu durdur
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null && playerHealth.isDead)
        {
            StopMovement(); // Oyuncu öldüðünde hareket ve saldýrýyý durdur
            return;
        }

        // Oyuncu ile olan mesafeyi hesapla
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Oyuncu saldýrý menzilindeyse saldýr
            animator.SetBool("isWalking", false);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("isAttacking"); // Saldýrý animasyonu tetikle
                lastAttackTime = Time.time; // Son saldýrý zamanýný güncelle
            }

            rb.velocity = Vector2.zero; // Oyuncu menzildeyse hareketi durdur
        }
        else
        {
            // Oyuncuya doðru hareket et
            animator.SetBool("isWalking", true);
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (isDead || player == null) return; // Ölü ise veya oyuncu yoksa hareket etmeye çalýþmasýn

        // Oyuncuya doðru hareket et
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Bossun yüzü oyuncuya dönsün
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // Sað tarafa bak
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Sol tarafa bak
        }
    }

    public void StopMovement()
    {
        isDead = true; // Boss'un öldüðünü iþaretle
        animator.SetBool("isWalking", false); // Yürüme animasyonunu durdur

        // Rigidbody'i durdur
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true; // Fiziksel etkileþim devam etsin
        }
    }
}
