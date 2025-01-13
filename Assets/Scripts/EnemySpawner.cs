using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Spawn edilecek düþman prefab'ý
    public List<Transform> spawnPoints; // Belirli spawn noktalarý
    public float spawnInterval = 10f; // Spawn aralýðý
    public int minEnemies = 5; // Minimum spawn edilecek düþman sayýsý
    public int maxEnemies = 10; // Maksimum spawn edilecek düþman sayýsý

    void Start()
    {
        // Spawn iþlemini sürekli olarak çaðýr
        InvokeRepeating(nameof(SpawnEnemies), 0f, spawnInterval);
    }

    void SpawnEnemies()
    {
        // Oyuncu öldüyse spawn iþlemini durdur
        if (PlayerHealth.isPlayerDead)
        {
            CancelInvoke(nameof(SpawnEnemies)); // Spawn iþlemini iptal et
            Debug.Log("Oyuncu öldü, düþman spawn durduruldu.");
            return;
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("Spawn noktalarý belirtilmemiþ!");
            return;
        }

        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            // Rastgele bir spawn noktasý seç
            Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Düþmaný spawn et
            Instantiate(enemyPrefab, selectedSpawnPoint.position, Quaternion.identity);
        }
    }
}
