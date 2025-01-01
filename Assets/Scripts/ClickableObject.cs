using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string objectName; // Bu nesnenin ad�
    public PuzzleManager puzzleManager; // PuzzleManager referans�
    private bool isPlayerNearby = false; // Oyuncu yak�nda m� kontrol�
    private bool hasShownText = false; // UI'nin daha �nce g�sterilip g�sterilmedi�ini kontrol eder

    private SpriteRenderer spriteRenderer; // Nesnenin SpriteRenderer'�
    private Color originalColor; // Nesnenin orijinal rengi

    public GameObject interactText; // UI Text GameObject'i

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Ba�lang�� rengini kaydet

        if (interactText != null)
        {
            interactText.SetActive(false); // Ba�lang��ta UI gizli olsun
        }
    }

    void Update()
    {
        // Oyuncu yak�nsa ve E tu�una basarsa t�klamay� tetikleyin
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
            spriteRenderer.color = Color.green; // Rengi ye�ile de�i�tir

            if (interactText != null && !hasShownText)
            {
                interactText.SetActive(true); // UI mesaj�n� g�ster
                Invoke(nameof(HideInteractText), 2f); // 2 saniye sonra UI'yi gizle
                hasShownText = true; // Art�k metin bir kez g�sterildi
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            spriteRenderer.color = originalColor; // Orijinal rengi geri y�kle
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
