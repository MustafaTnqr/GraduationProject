using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int damage = 25;

    void Start()
    {
        // Mermiyi 5 saniye sonra yok et
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer EnemyHealth'e sahip bir objeye çarptýysa
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Eðer BossHealth'e sahip bir objeye çarptýysa
        BossHealth bossHealth = collision.GetComponent<BossHealth>();
        if (bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Eðer baþka bir þeyle çarptýysa mermiyi yok et
        Destroy(gameObject);
    }
}
