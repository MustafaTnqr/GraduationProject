using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string objectName; // Bu nesnenin adý
    public PuzzleManager puzzleManager; // PuzzleManager referansý
    private bool isPlayerNearby = false; // Oyuncu yakýnda mý kontrolü
    private bool hasShownText = false; // UI'nin daha önce gösterilip gösterilmediðini kontrol eder

    private SpriteRenderer spriteRenderer; // Nesnenin SpriteRenderer'ý
    private Color originalColor; // Nesnenin orijinal rengi

    public GameObject interactText; // UI Text GameObject'i

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Baþlangýç rengini kaydet

        if (interactText != null)
        {
            interactText.SetActive(false); // Baþlangýçta UI gizli olsun
        }
    }

    void Update()
    {
        // Oyuncu yakýnsa ve E tuþuna basarsa týklamayý tetikleyin
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            puzzleManager.CheckPuzzle(objectName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu etiketiyle kontrol
        {
            isPlayerNearby = true;
            spriteRenderer.color = Color.green; // Rengi yeþile deðiþtir

            if (interactText != null && !hasShownText)
            {
                interactText.SetActive(true); // UI mesajýný göster
                Invoke(nameof(HideInteractText), 2f); // 2 saniye sonra UI'yi gizle
                hasShownText = true; // Artýk metin bir kez gösterildi
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            spriteRenderer.color = originalColor; // Orijinal rengi geri yükle
        }
    }

    void HideInteractText()
    {
        if (interactText != null)
        {
            interactText.SetActive(false); // UI metnini gizle
        }
    }
}
