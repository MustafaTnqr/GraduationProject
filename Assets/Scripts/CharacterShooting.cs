using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Kurþun prefabý
    public Transform firePoint; // Kurþunun çýkýþ noktasý
    public float bulletSpeed = 10f; // Kurþunun hýzý
    private Animator animator; // Animator referansý
    private Rigidbody2D rb; // Rigidbody referansý
    private bool isAttacking = false; // Saldýrý kontrolü

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator'ý al
        rb = GetComponent<Rigidbody2D>(); // Rigidbody'yi al
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Hareket girdilerini al
            float moveInputX = Input.GetAxisRaw("Horizontal");
            float moveInputY = Input.GetAxisRaw("Vertical");
            Vector2 moveDirection = new Vector2(moveInputX, moveInputY).normalized;

            // Rigidbody hýzýný ayarla
            rb.velocity = moveDirection * 5f; // Hareket hýzý

            // Animator'a hareket bilgisi gönder
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

        // Mouse týklanýrsa ve saldýrý devam etmiyorsa
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        isAttacking = true; // Saldýrý durumunu aktif et
        animator.SetTrigger("Attack"); // Saldýrý animasyonunu tetikle

        // Hareketi durdur
        rb.velocity = Vector2.zero; // Karakteri tamamen durdur

        // Mouse pozisyonunu bul
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini sýfýrla (2D için gerekli)

        // FirePoint'ten mouse pozisyonuna doðru yön hesapla
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Kurþunu oluþtur ve yönünü ayarla
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.velocity = direction * bulletSpeed;

        // Animasyon tamamlanýnca tekrar saldýrý yapmayý aktif et
        Invoke(nameof(ResetAttack), 0.5f); // Saldýrý tamamlanma süresine göre ayarla
    }

    void ResetAttack()
    {
        isAttacking = false; // Saldýrý tekrar yapýlabilir
    }
}
