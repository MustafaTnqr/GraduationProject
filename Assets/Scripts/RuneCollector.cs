using UnityEngine;
using UnityEngine.SceneManagement;

public class RuneCollector : MonoBehaviour
{
    public string mainMapSceneName = "MainMap"; // Ana haritan�n sahne ad�
    private bool runeCollected = false; // R�n topland� m� kontrol�

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rune") && !runeCollected)
        {
            runeCollected = true;

            // R�n toplan�nca nesneyi yok et
            Destroy(other.gameObject);

            Debug.Log("R�n topland�!");

            // Ana haritaya d�n
            GoToMainMap();
        }
    }

    void GoToMainMap()
    {
        // Ana haritaya sahneyi y�kle
        SceneManager.LoadScene(mainMapSceneName);
    }
}
