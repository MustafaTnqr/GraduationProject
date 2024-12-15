using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Spawn edilecek d��man prefab'�
    public Transform player; // Karakterin pozisyonu
    public float spawnRadius = 15f; // Daire �ap� (yar��ap)
    public float minSpawnDistance = 7f; // Minimum spawn mesafesi
    public float spawnInterval = 10f; // Spawn aral���
    public int minEnemies = 5; // Minimum spawn edilecek d��man say�s�
    public int maxEnemies = 10; // Maksimum spawn edilecek d��man say�s�

    void Start()
    {
        // Spawn i�lemini s�rekli olarak �a��r
        InvokeRepeating(nameof(SpawnEnemies), 0f, spawnInterval);
    }

    void SpawnEnemies()
    {
        // Oyuncu �ld�yse spawn i�lemini durdur
        if (PlayerHealth.isPlayerDead)
        {
            CancelInvoke(nameof(SpawnEnemies)); // Spawn i�lemini iptal et
            Debug.Log("Oyuncu �ld�, d��man spawn durduruldu.");
            return;
        }

        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition;

            // Karaktere olan mesafeyi kontrol ederek ge�erli bir pozisyon bul
            do
            {
                Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
                spawnPosition = new Vector3(player.position.x + randomPosition.x, player.position.y + randomPosition.y, 0);
            }
            while (Vector3.Distance(spawnPosition, player.position) < minSpawnDistance);

            // D��man� spawn et
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
