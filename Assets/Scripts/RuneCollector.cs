using UnityEngine;
using UnityEngine.SceneManagement;

public class RuneCollector : MonoBehaviour
{
    public string mainMapSceneName; // Ana map sahne adý
    public string runeName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.AddRune(runeName); // Rünü ekle
                Debug.Log($"Rün toplandý: {runeName}");
                SceneManager.LoadScene(mainMapSceneName); // Ana mape ýþýnla
            }
            else
            {
                Debug.LogError("GameManager bulunamadý!");
            }
        }
    }
}
