using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public GameObject uiPanel; // Mesajlarý gösterecek panel
    public Text messageText; // Mesaj metnini gösterecek Text bileþeni
    public string[] messages; // Gösterilecek mesajlar
    public KeyCode nextKey = KeyCode.Space; // Mesajlarý geçmek için tuþ
    public GameObject triggerZone; // Trigger zone objesi

    private int currentMessageIndex = 0; // Þu anki mesajýn indeksi
    private bool isShowingMessage = false; // Mesaj gösterim durumu

    private void Start()
    {
        // Mesaj daha önce gösterilmiþ mi kontrol et
        if (PlayerPrefs.GetInt("HasMessageShown", 0) == 1)
        {
            if (triggerZone != null)
            {
                triggerZone.SetActive(false); // Trigger zone'u devre dýþý býrak
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerPrefs.GetInt("HasMessageShown", 0) == 0)
        {
            ShowMessage();
            PlayerPrefs.SetInt("HasMessageShown", 1); // Mesajýn gösterildiðini kaydet
            if (triggerZone != null)
            {
                triggerZone.SetActive(false); // Mesaj gösterildikten sonra trigger zone'u devre dýþý býrak
            }
        }
    }

    private void Update()
    {
        // Eðer mesaj gösteriliyorsa ve oyuncu belirlenen tuþa basýyorsa
        if (isShowingMessage && Input.GetKeyDown(nextKey))
        {
            ShowNextMessage();
        }
    }

    private void ShowMessage()
    {
        if (uiPanel != null && messages.Length > 0)
        {
            isShowingMessage = true; // Mesaj gösterilmeye baþlandý
            currentMessageIndex = 0; // Ýlk mesajdan baþla
            uiPanel.SetActive(true); // Paneli aktif et
            messageText.text = messages[currentMessageIndex]; // Ýlk mesajý göster
        }
    }

    private void ShowNextMessage()
    {
        currentMessageIndex++;

        if (currentMessageIndex < messages.Length)
        {
            // Bir sonraki mesajý göster
            messageText.text = messages[currentMessageIndex];
        }
        else
        {
            // Tüm mesajlar bittiðinde paneli kapat
            isShowingMessage = false;
            if (uiPanel != null)
            {
                uiPanel.SetActive(false);
            }
        }
    }
}
