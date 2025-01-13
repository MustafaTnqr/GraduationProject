using UnityEngine;
using TMPro;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // Mesajlarý gösterecek TextMeshPro
    public string[] messages; // Gösterilecek mesajlar
    public float typingSpeed = 0.05f; // Yazý tipi hýzý
    public GameObject uiPanel; // Mesajlarýn gösterileceði panel

    private int index = 0; // Þu anki mesaj indeksi
    private bool isTyping = false; // Yazý yazýlýyor mu?
    private bool allMessagesShown = false; // Tüm mesajlarýn gösterilip gösterilmediði
    private static bool hasShownMessage = false; // Oyunun baþlangýcýnda bir kez gösterim kontrolü

    private void Start()
    {
        if (!hasShownMessage && messages.Length > 0 && uiPanel != null)
        {
            hasShownMessage = true; // Mesajýn bir kez gösterildiðini iþaretle
            uiPanel.SetActive(true); // Paneli aç
            StartCoroutine(TypeMessage());
        }
        else
        {
            // Mesaj daha önce gösterildiyse paneli kapalý tut
            if (uiPanel != null)
            {
                uiPanel.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !allMessagesShown) // Oyuncu "Space" tuþuna bastýðýnda
        {
            if (isTyping) // Eðer yazý yazýlýyorsa hemen bitir
            {
                CompleteTyping();
            }
            else // Eðer yazý tamamlandýysa bir sonraki mesaja geç
            {
                NextMessage();
            }
        }
    }

    private IEnumerator TypeMessage()
    {
        isTyping = true;
        textDisplay.text = "";

        foreach (char letter in messages[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false; // Yazý yazýmý tamamlandý
    }

    private void CompleteTyping()
    {
        StopAllCoroutines();
        textDisplay.text = messages[index]; // Mesajýn tamamýný göster
        isTyping = false;
    }

    public void NextMessage()
    {
        if (index < messages.Length - 1)
        {
            index++;
            StartCoroutine(TypeMessage());
        }
        else
        {
            allMessagesShown = true; // Tüm mesajlar gösterildi
            ClosePanel(); // Paneli kapat
        }
    }

    private void ClosePanel()
    {
        Debug.Log("Tüm mesajlar gösterildi.");
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // Paneli kapat
            textDisplay.text = ""; // Son mesajý temizle
        }
    }
}
