using UnityEngine;

public class LightSource : MonoBehaviour
{
    public float beamLength = 50f; // Iþýðýn maksimum uzunluðu
    public LayerMask reflectionLayers;
    public LayerMask obstacleLayer;// Yansýma yapacak katmanlar
    public int maxReflections = 5; // Maksimum yansýma sayýsý
    public LineRenderer lineRenderer; // Iþýðýn görselleþtirilmesi için LineRenderer
    private bool hasReachedTarget = false; // Hedefe ulaþýldý mý kontrolü

    private void Update()
    {
        // Eðer hedefe ulaþýldýysa ýþýðý tamamen durdur
        if (hasReachedTarget) return;

        // Iþýðý yansýt ve hedefe git
        CastLight(transform.position, transform.right, maxReflections);
    }

    void CastLight(Vector2 start, Vector2 direction, int remainingReflections)
    {
        if (remainingReflections <= 0) return;

        Vector2 currentStart = start;
        Vector2 currentDirection = direction;

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentStart, currentDirection, beamLength, reflectionLayers | obstacleLayer);

            if (hit.collider != null)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);

                // Engel kontrolü
                if (((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0)
                {
                    Debug.Log($"Engel tarafýndan bloklandý: {hit.collider.name}");
                    break;
                }

                // Hedef kontrolü
                if (hit.collider.CompareTag("Target"))
                {
                    Debug.Log($"Hedefe ulaþýldý: {hit.collider.name}");
                    TriggerTargetEvent(hit.collider.gameObject);
                    hasReachedTarget = true;
                    StopLight();
                    break;
                }

                // Yansýma iþlemi
                if (hit.collider.CompareTag("Mirror"))
                {
                    currentDirection = Vector2.Reflect(currentDirection, hit.normal);
                    currentStart = hit.point + hit.normal * 0.01f;
                }
            }
            else
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentStart + currentDirection * beamLength);
                break;
            }
        }
    }


    // Hedefe ulaþýldýðýnda bir olay tetikle
    private void TriggerTargetEvent(GameObject target)
    {
        LightTarget lightTarget = target.GetComponent<LightTarget>();
        if (lightTarget != null)
        {
            lightTarget.PuzzleCompleted(); // Hedefteki metodu çaðýr
        }
    }

    // Iþýðý tamamen durdur
    private void StopLight()
    {
        lineRenderer.enabled = false; // Iþýðýn görselliðini kapat
        this.enabled = false; // LightSource scriptini tamamen devre dýþý býrak
        Debug.Log("Iþýk kaynaðý tamamen durduruldu.");
    }
}
