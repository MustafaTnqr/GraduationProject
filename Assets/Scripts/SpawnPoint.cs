using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Karakter prefab'�
    public Transform spawnPoint; // Spawn noktas�

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

            // Kameray� spawn edilen karaktere ba�la
            Camera.main.GetComponent<CameraFollow>().SetTarget(spawnedPlayer.transform);
        }
    }
}
