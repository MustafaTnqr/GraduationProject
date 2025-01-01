using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<string> correctOrder; // Doðru týklama sýrasý
    private int currentIndex = 0; // Mevcut týklama sýrasý

    public GameObject portal; // Portalýn referansý

    public void CheckPuzzle(string clickedObjectName)
    {
        // Eðer týklanan doðru sýrada deðilse sýfýrla
        if (currentIndex >= correctOrder.Count || clickedObjectName != correctOrder[currentIndex])
        {
            Debug.Log("Yanlýþ kombinasyon. Puzzle sýfýrlandý!");
            currentIndex = 0;
            return;
        }

        // Doðru sýradaysa bir sonrakine geç
        Debug.Log($"Doðru sýrada týklandý: {clickedObjectName}");
        currentIndex++;

        // Eðer tüm doðru sýralama tamamlanýrsa puzzle tamamlandý
        if (currentIndex == correctOrder.Count)
        {
            Debug.Log("Puzzle tamamlandý!");
            ActivatePortal();
        }
    }

    private void ActivatePortal()
    {
        if (portal != null)
        {
            portal.SetActive(true); // Portalý aktif et
        }
    }
}
