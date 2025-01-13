using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.BoolParameter;


public class BossInfo : MonoBehaviour
{
    public GameObject uiPanel; // Mesajý gösterecek olan panel
    public float displayTime = 5f; // Mesajýn ne kadar süre görünür kalacaðý

    private bool hasTriggered = false; // Mesaj daha önce gösterildi mi kontrolü

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player")) // Sadece oyuncu ve ilk giriþte tetiklenir
        {
            hasTriggered = true;
            ShowMessage();
        }
    }

    private void ShowMessage()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true); // Paneli göster
            Invoke(nameof(HideMessage), displayTime); // Belirli bir süre sonra gizle
        }
    }

    private void HideMessage()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // Paneli gizle
        }
    }
}