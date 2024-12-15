using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hýzý
    private Rigidbody2D rb; // Rigidbody referansý
    private Vector2 moveDirection;
    private Animator animator; // Animator referansý

    private Vector3 originalScale; // Karakterin orijinal scale deðeri

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody atanýr
        animator = GetComponent<Animator>(); // Animator atanýr
        originalScale = transform.localScale; // Orijinal scale deðerini kaydet
    }

    void Update()
    {
        // Klavye giriþlerini al
        float moveInputX = Input.GetAxisRaw("Horizontal"); // Saða-sola hareket
        float moveInputY = Input.GetAxisRaw("Vertical"); // Yukarý-aþaðý hareket
        moveDirection = new Vector2(moveInputX, moveInputY).normalized;

        // Animator'a hýz bilgisini gönder
        animator.SetFloat("Speed", moveDirection.magnitude); // Hýz parametresi
        animator.SetFloat("Horizontal", moveInputX); // X ekseni hareket
        animator.SetFloat("Vertical", moveInputY); // Y ekseni hareket

        // Karakterin yüzünü hareket yönüne göre döndür (Scale'i koruyarak)
        if (moveInputX > 0) // Saða gidiyorsa
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (moveInputX < 0) // Sola gidiyorsa
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }

    void FixedUpdate()
    {
        // Rigidbody ile hareket
        rb.velocity = moveDirection * moveSpeed;
    }
}
