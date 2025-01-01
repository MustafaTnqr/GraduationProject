using UnityEngine;
using UnityEngine.SceneManagement;

public class RuneCollector : MonoBehaviour
{
    public string mainMapSceneName = "MainMap"; // Ana haritanýn sahne adý
    private bool runeCollected = false; // Rün toplandý mý kontrolü

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rune") && !runeCollected)
        {
            runeCollected = true;

            // Rün toplanýnca nesneyi yok et
            Destroy(other.gameObject);

            Debug.Log("Rün toplandý!");

            // Ana haritaya dön
            GoToMainMap();
        }
    }

    void GoToMainMap()
    {
        // Ana haritaya sahneyi yükle
        SceneManager.LoadScene(mainMapSceneName);
    }
}
