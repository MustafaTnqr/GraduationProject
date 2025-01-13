using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> collectedRunes = new List<string>(); // Toplanan rünler
    public List<string> placedRunes = new List<string>(); // Yerleþtirilen rünler

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddRune(string runeName)
    {
        if (!collectedRunes.Contains(runeName))
        {
            collectedRunes.Add(runeName);
            Debug.Log($"Rün toplandý: {runeName}");
        }
    }

    public void PlaceRune(string runeName)
    {
        if (!placedRunes.Contains(runeName))
        {
            placedRunes.Add(runeName);
            Debug.Log($"Rün yerleþtirildi: {runeName}");
        }
    }

    public bool IsRunePlaced(string runeName)
    {
        return placedRunes.Contains(runeName);
    }
}
