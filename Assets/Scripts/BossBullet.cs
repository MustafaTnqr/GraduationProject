using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public int damage = 10; // Merminin vereceði hasar
    public float lifetime = 5f; // Merminin ömrü
    public float bulletSpeed = 10f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Belirli bir süre sonra yok et
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Oyuncuya hasar ver
            }
            Destroy(gameObject); // Mermiyi yok et
        }
        
    }
}
