using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Kur�un prefab�
    public Transform firePoint; // Kur�unun ��k�� noktas�
    public float bulletSpeed = 10f; // Kur�unun h�z�
    private Animator animator; // Animator referans�
    private Rigidbody2D rb; // Rigidbody referans�
    private bool isAttacking = false; // Sald�r� kontrol�

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator'� al
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

            // Rigidbody h�z�n� ayarla
            rb.velocity = moveDirection * 5f; // Hareket h�z�

            // Animator'a hareket bilgisi g�nder
            animator.SetFloat("Speed", moveDirection.magnitude);
        }

        // Mouse t�klan�rsa ve sald�r� devam etmiyorsa
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        isAttacking = true; // Sald�r� durumunu aktif et
        animator.SetTrigger("Attack"); // Sald�r� animasyonunu tetikle

        // Hareketi durdur
        rb.velocity = Vector2.zero; // Karakteri tamamen durdur

        // Mouse pozisyonunu bul
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z eksenini s�f�rla (2D i�in gerekli)

        // FirePoint'ten mouse pozisyonuna do�ru y�n hesapla
        Vector2 direction = (mousePosition - firePoint.position).normalized;

        // Kur�unu olu�tur ve y�n�n� ayarla
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.velocity = direction * bulletSpeed;

        // Animasyon tamamlan�nca tekrar sald�r� yapmay� aktif et
        Invoke(nameof(ResetAttack), 0.5f); // Sald�r� tamamlanma s�resine g�re ayarla
    }

    void ResetAttack()
    {
        isAttacking = false; // Sald�r� tekrar yap�labilir
    }
}
