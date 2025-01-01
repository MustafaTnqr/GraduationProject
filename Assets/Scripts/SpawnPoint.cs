using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Karakter prefab'ý
    public Transform spawnPoint; // Spawn noktasý

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

            // Kamerayý spawn edilen karaktere baðla
            Camera.main.GetComponent<CameraFollow>().SetTarget(spawnedPlayer.transform);
        }
    }
}
