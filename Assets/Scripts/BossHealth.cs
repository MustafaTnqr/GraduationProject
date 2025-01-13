using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 300; // Maksimum can
    private int currentHealth;

    private Animator animator;
    private SpriteRenderer spriteRenderer; // Rengi deðiþtirmek için SpriteRenderer
    private bool isDead = false; // Ölüm kontrolü
    private bool isEnraged = false; // Parlama durumu kontrolü

    public GameObject winScreen; // Bitiþ ekraný referansý
    public Slider healthBar; // UI Saðlýk barý
    public Color enragedColor = Color.magenta; // Parlama rengi
    public float glowSpeed = 2f; // Parlama hýzýný kontrol etmek için

    private void Start()
    {
        currentHealth = maxHealth; // Baþlangýçta tam can
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }

        if (winScreen != null)
        {
            winScreen.SetActive(false); // Oyun baþýnda bitiþ ekranýný gizle
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Eðer boss öldüyse hasar almasýný engelle

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Canýn sýfýrýn altýna düþmesini engelle

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= maxHealth / 2 && !isEnraged)
        {
            EnterEnragedState(); // Yarý cana düþtüðünde parlama baþlasýn
        }

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("isHit"); // Hasar aldýðýnda animasyonu tetikle
        }
    }

    private void EnterEnragedState()
    {
        isEnraged = true; // Artýk mor parlama baþladý
        Debug.Log("Boss enraged! Mor parlama baþladý.");
        StartCoroutine(GlowEffect());
    }

    private System.Collections.IEnumerator GlowEffect()
    {
        float t = 0;

        while (isEnraged && !isDead)
        {
            t += Time.deltaTime * glowSpeed;

            // Mor parlama efekti (lerp ile renklendirme)
            spriteRenderer.color = Color.Lerp(Color.white, enragedColor, Mathf.PingPong(t, 1));

            yield return null;
        }

        spriteRenderer.color = Color.white; // Öldüðünde normal renge dön
    }

    private void Die()
    {
        if (isDead) return; // Eðer boss zaten ölü ise iþlemi tekrar etme

        isDead = true; // Boss'un öldüðünü iþaretle
        Debug.Log("Boss died!");
        animator.SetTrigger("isDead"); // Ölüm animasyonu tetikle

        // Hareketi durdur
        BossMovement movementScript = GetComponent<BossMovement>();
        if (movementScript != null)
        {
            movementScript.StopMovement();
            movementScript.enabled = false; // Scripti tamamen devre dýþý býrak
        }

        // Saðlýk barýný devre dýþý býrak
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false);
        }

        // Bitiþ ekranýný gecikmeli olarak aç
        StartCoroutine(ShowWinScreenWithDelay());

        // Parlama efektini durdur
        isEnraged = false;
    }

    private System.Collections.IEnumerator ShowWinScreenWithDelay()
    {
        yield return new WaitForSeconds(3f); // 5 saniye bekle

        if (winScreen != null)
        {
            winScreen.SetActive(true); // Bitiþ ekranýný görünür yap
        }
    }
}
