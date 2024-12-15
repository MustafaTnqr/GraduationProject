using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int damage = 25;
    void Start()
    {
        // 5 saniye sonra mermiyi yok et
        Destroy(gameObject, 5f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // EnemyHealth script'ini bul ve hasar uygula
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            // Mermiyi yok et
            Destroy(gameObject);
        }

        // Eðer baþka bir obje ile çarpýþtýysa da yok olmasýný istiyorsanýz:
        else
        {
            Destroy(gameObject);
        }
    }
}
