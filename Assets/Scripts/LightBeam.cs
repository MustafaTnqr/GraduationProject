using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightBeam : MonoBehaviour
{
    public Transform lightSource; // Iþýðýn baþlangýç noktasý
    public int maxReflections = 5; // Maksimum yansýma sayýsý
    public float maxDistance = 50f; // Iþýðýn maksimum mesafesi
    public LayerMask reflectionLayers; // Yansýma ve engelleme yapacak katmanlar
    public string targetTag = "Target"; // Hedefin tag'i

    private LineRenderer lineRenderer;
    private bool hasHitTarget = false; // Hedefe ulaþýldý mý kontrolü

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (!hasHitTarget) // Hedefe ulaþýldýysa ýþýðý durdur
        {
            SimulateLight();
        }
    }

    void SimulateLight()
    {
        Vector3 startPosition = lightSource.position; // Iþýðýn baþlangýç pozisyonu
        Vector3 direction = lightSource.right; // Iþýðýn yönü (saða bakýyorsa)

        List<Vector3> lightPoints = new List<Vector3> { startPosition }; // Iþýðýn geçtiði noktalar
        int reflections = 0;

        while (reflections < maxReflections)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, maxDistance, reflectionLayers);

            Debug.DrawRay(startPosition, direction * maxDistance, Color.red, 0.1f); // Raycast'i görselleþtir

            if (hit.collider != null)
            {
                lightPoints.Add(hit.point);

                // Eðer hedef objesine çarptýysa
                if (hit.collider.CompareTag(targetTag))
                {
                    if (!hasHitTarget) // Daha önce hedefe ulaþýlmamýþsa
                    {
                        Debug.Log("Iþýk hedefe ulaþtý!");
                        hasHitTarget = true; // Hedefe ulaþýldý olarak iþaretle
                        hit.collider.GetComponent<PuzzleManager2>()?.PuzzleCompleted();
                    }
                    break; // Ýþlem tamamlandýktan sonra döngüyü sonlandýr
                }

                // Eðer bir obstacle'a çarptýysa
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
                {
                    Debug.Log("Iþýk bir engelle çarpýþtý!");
                    break; // Obstacle'a çarptýðýnda ýþýk durmalý
                }

                // Aynadan yansýma
                direction = Vector2.Reflect(direction, hit.normal);
                startPosition = hit.point;

                reflections++;
            }
            else
            {
                // Çarpmadýysa ýþýðýn maksimum mesafesine kadar çiz
                lightPoints.Add(startPosition + direction * maxDistance);
                break;
            }
        }

        // Iþýk çizgisini güncelle
        lineRenderer.positionCount = lightPoints.Count;
        lineRenderer.SetPositions(lightPoints.ToArray());
    }
}
