using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket h�z�
    private Rigidbody2D rb; // Rigidbody referans�
    private Vector2 moveDirection;
    private Animator animator; // Animator referans�

    private Vector3 originalScale; // Karakterin orijinal scale de�eri

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody atan�r
        animator = GetComponent<Animator>(); // Animator atan�r
        originalScale = transform.localScale; // Orijinal scale de�erini kaydet
    }

    void Update()
    {
        // Klavye giri�lerini al
        float moveInputX = Input.GetAxisRaw("Horizontal"); // Sa�a-sola hareket
        float moveInputY = Input.GetAxisRaw("Vertical"); // Yukar�-a�a�� hareket
        moveDirection = new Vector2(moveInputX, moveInputY).normalized;

        // Animator'a h�z bilgisini g�nder
        animator.SetFloat("Speed", moveDirection.magnitude); // H�z parametresi
        animator.SetFloat("Horizontal", moveInputX); // X ekseni hareket
        animator.SetFloat("Vertical", moveInputY); // Y ekseni hareket

        // Karakterin y�z�n� hareket y�n�ne g�re d�nd�r (Scale'i koruyarak)
        if (moveInputX > 0) // Sa�a gidiyorsa
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
