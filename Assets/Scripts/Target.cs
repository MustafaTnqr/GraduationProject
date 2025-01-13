using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için gerekli

public class LightTarget : MonoBehaviour
{
    private bool isTargetHit = false; // Hedef daha önce vurulmuþ mu?
    private SpriteRenderer spriteRenderer; // SpriteRenderer referansý

    public Color targetGlowColor = Color.yellow; // Hedefin parlayacaðý renk
    public string nextSceneName = "WaterLevel"; // Geçilecek sahnenin adý

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer bulunamadý! Bu hedefte parlama olmayacak.");
        }
    }

    public void PuzzleCompleted()
    {
        if (isTargetHit) return; // Eðer hedef daha önce vurulduysa hiçbir þey yapma

        isTargetHit = true;
        Debug.Log("Puzzle tamamlandý! Hedef parladý!");
        StartGlow(); // Parlama efektini baþlat

        StartCoroutine(LoadNextSceneWithDelay(1f)); // 1 saniye gecikme ile sahne geçiþi
    }

    private void StartGlow()
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(GlowEffect()); // Parlama efektini baþlat
        }
        else
        {
            Debug.LogWarning("SpriteRenderer bulunamadý!");
        }
    }

    private System.Collections.IEnumerator GlowEffect()
    {
        float glowDuration = 1f; // Parlama süresi
        Color initialColor = spriteRenderer.color;

        // Parlama efektini uygula
        for (float t = 0; t < glowDuration; t += Time.deltaTime)
        {
            spriteRenderer.color = Color.Lerp(initialColor, targetGlowColor, t / glowDuration);
            yield return null;
        }

        spriteRenderer.color = targetGlowColor; // Parlama efektini tamamla
        Debug.Log("Hedef parladý!");
    }

    private System.Collections.IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen süre kadar bekle
        Debug.Log($"Sahneye geçiliyor: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName); // Sahneyi yükle
    }
}
