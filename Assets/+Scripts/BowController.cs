using UnityEngine;

public class BowController : MonoBehaviour
{
    public GameObject arrowPrefab; // Префаб стрелы.
    public Transform shootPoint;   // Точка выстрела.
    public float shootInterval = 2f; // Интервал между выстрелами.

    private float shootTimer;

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        ArrowController arrowController = arrow.GetComponent<ArrowController>();
        arrowController.SetDirection(Vector3.right); // Направление стрелы (зависит от расположения лука).
    }
}
