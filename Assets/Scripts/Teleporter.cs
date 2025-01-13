using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþtirmek için gerekli

public class LevelTeleporter : MonoBehaviour
{
    [Header("Target Level")]
    public string targetSceneName; // Iþýnlanacaðýnýz sahnenin adý

    [Header("UI Feedback")]
    public GameObject teleportMessage; // Oyuncuya mesaj göstermek için UI elemaný (isteðe baðlý)

    private void Start()
    {
        // UI mesajýný baþta gizle
        if (teleportMessage != null)
        {
            teleportMessage.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Sadece Player objesiyle çalýþýr
        {
            if (teleportMessage != null)
            {
                teleportMessage.SetActive(true); // Mesajý göster
            }

            // Sahneye geçiþi biraz geciktirmek için Coroutine kullanabilirsiniz
            StartCoroutine(TeleportPlayer());
        }
    }

    private System.Collections.IEnumerator TeleportPlayer()
    {
        // Ýsteðe baðlý: Geçiþ efekti veya bekleme süresi ekleyebilirsiniz
        yield return new WaitForSeconds(1f);

        // Hedef sahneye geçiþ yap
        SceneManager.LoadScene(targetSceneName);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (teleportMessage != null)
            {
                teleportMessage.SetActive(false); // Mesajý gizle
            }
        }
    }
}
