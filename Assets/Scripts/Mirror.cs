using UnityEngine;

public class Mirror : MonoBehaviour
{
    private bool isDragging = false; // Sað týk basýlý mý kontrolü
    private float rotationSpeed = 300f; // Döndürme hýzý (arttýrýlmýþ hassasiyet)

    private void Update()
    {
        // Sað týk basýlýysa döndürme baþlar
        if (Input.GetMouseButtonDown(1)) // Sað týk
        {
            // Fare ýþýnýný dünyaya dönüþtür
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButton(1) && isDragging)
        {
            // Fare hareketine baðlý döndürme
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.forward, -mouseX * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(1)) // Sað týk býrakýldýðýnda
        {
            isDragging = false;
        }
    }
}
