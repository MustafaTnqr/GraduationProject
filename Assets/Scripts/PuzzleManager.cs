using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<string> correctOrder; // Do�ru t�klama s�ras�
    private int currentIndex = 0; // Mevcut t�klama s�ras�

    public GameObject portal; // Portal�n referans�

    public void CheckPuzzle(string clickedObjectName)
    {
        // E�er t�klanan do�ru s�rada de�ilse s�f�rla
        if (currentIndex >= correctOrder.Count || clickedObjectName != correctOrder[currentIndex])
        {
            Debug.Log("Yanl�� kombinasyon. Puzzle s�f�rland�!");
            currentIndex = 0;
            return;
        }

        // Do�ru s�radaysa bir sonrakine ge�
        Debug.Log($"Do�ru s�rada t�kland�: {clickedObjectName}");
        currentIndex++;

        // E�er t�m do�ru s�ralama tamamlan�rsa puzzle tamamland�
        if (currentIndex == correctOrder.Count)
        {
            Debug.Log("Puzzle tamamland�!");
            ActivatePortal();
        }
    }

    private void ActivatePortal()
    {
        if (portal != null)
        {
            portal.SetActive(true); // Portal� aktif et
        }
    }
}
