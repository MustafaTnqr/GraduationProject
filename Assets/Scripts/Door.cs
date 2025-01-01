using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string targetScene; // Hedef sahne ad�

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Oyuncu kap�ya dokundu mu kontrol et
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene); // Hedef sahneye ���nla
        }
        else
        {
            Debug.LogWarning("Target scene name is not set!");
        }
    }
}
