using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Spawn edilecek düþman prefab'ý
    public Transform player; // Karakterin pozisyonu
    public float spawnRadius = 15f; // Daire çapý (yarýçap)
    public float minSpawnDistance = 7f; // Minimum spawn mesafesi
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

        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition;

            // Karaktere olan mesafeyi kontrol ederek geçerli bir pozisyon bul
            do
            {
                Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
                spawnPosition = new Vector3(player.position.x + randomPosition.x, player.position.y + randomPosition.y, 0);
            }
            while (Vector3.Distance(spawnPosition, player.position) < minSpawnDistance);

            // Düþmaný spawn et
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
