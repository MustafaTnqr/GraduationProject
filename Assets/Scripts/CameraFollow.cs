using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek obje
    public Vector3 offset; // Kamera ile hedef aras�ndaki mesafe
    public float smoothSpeed = 0.125f; // Kamera hareket h�z�

    void LateUpdate()
    {
        if (target == null) return;

        // Hedef pozisyonunu hesapla
        Vector3 desiredPosition = target.position + offset;
        // Kameray� yumu�ak bir �ekilde hareket ettir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 2D oyunlar i�in Z ekseninde sabit kal
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
