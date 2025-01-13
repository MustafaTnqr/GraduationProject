using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'ý
    public Transform firePoint; // Merminin çýkacaðý nokta
    public float attackRange = 10f; // Saldýrý menzili
    public float bulletSpeed = 10f;

    private Transform player; // Oyuncunun konumu
    private bool canShoot = false; // Ateþ etme kontrolü (animasyon tarafýndan tetiklenecek)

    private void Update()
    {
        if (PlayerHealth.isPlayerDead) return; // Eðer oyuncu ölü ise hiçbir þey yapma

        // Oyuncuyu bul
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                return; // Oyuncu sahnede yoksa çýk
            }
        }

        // Oyuncu menzildeyse saldýr
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            // Ateþ etme kontrolü animasyon tarafýndan yapýlacak
            canShoot = true;
        }
        else
        {
            canShoot = false; // Oyuncu menzilden çýktýysa ateþ etmeyi durdur
        }
    }

    // Animasyon olayý tarafýndan çaðrýlacak
    public void FireBullet()
    {
        if (canShoot && bulletPrefab != null && firePoint != null)
        {
            // Mermiyi oluþtur ve yönlendir
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (player.position - firePoint.position).normalized;

            // Mermiye hareket ekle
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            Debug.Log("Boss mermi attý!");
        }
    }
}
