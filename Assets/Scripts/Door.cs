using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string targetScene; // Hedef sahne adý

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu kapýya dokundu mu kontrol et
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene); // Hedef sahneye ýþýnla
        }
        else
        {
            Debug.LogWarning("Target scene name is not set!");
        }
    }
}
