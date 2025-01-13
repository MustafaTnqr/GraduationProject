using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþtirme için gerekli

public class RunePalette : MonoBehaviour
{
    [System.Serializable]
    public class RuneSlot
    {
        public string runeName; // Gerekli rün ismi
        public GameObject runeVisual; // Rüne ait görsel
    }

    public List<RuneSlot> runeSlots = new List<RuneSlot>(); // Rün slotlarý
    public string nextSceneName; // Iþýnlanýlacak sahne ismi

    private void Start()
    {
        foreach (var slot in runeSlots)
        {
            if (GameManager.instance != null && GameManager.instance.IsRunePlaced(slot.runeName))
            {
                Debug.Log($"Rün zaten yerleþtirilmiþ: {slot.runeName}");
                slot.runeVisual.SetActive(true); // Zaten yerleþtirildiyse görünür yap
            }
            else
            {
                slot.runeVisual.SetActive(false); // Henüz yerleþtirilmediyse gizle
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool allRunesPlaced = true;

            foreach (var slot in runeSlots)
            {
                if (!GameManager.instance.IsRunePlaced(slot.runeName) &&
                    GameManager.instance.collectedRunes.Contains(slot.runeName))
                {
                    Debug.Log($"Rün baþarýyla yerleþtirildi: {slot.runeName}");
                    GameManager.instance.PlaceRune(slot.runeName); // Rünü yerleþtirilmiþ olarak iþaretle
                    slot.runeVisual.SetActive(true); // Görseli aktif et
                }

                // Eðer rün yerleþtirilmemiþse, hepsi tamamlanmýþ deðil
                if (!GameManager.instance.IsRunePlaced(slot.runeName))
                {
                    allRunesPlaced = false;
                }
            }

            // Eðer tüm rünler yerleþtirildiyse sahne deðiþtir
            if (allRunesPlaced && !string.IsNullOrEmpty(nextSceneName))
            {
                Debug.Log("Tüm rünler yerleþtirildi! Bir sonraki sahneye ýþýnlanýyor...");
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
