using UnityEngine;

public class BowController : MonoBehaviour
{
    private Transform arrow; // Ссылка на стрелу (первый дочерний объект).
    public float shootInterval = 2f; // Интервал между выстрелами.
    private float shootTimer; // Таймер для отслеживания времени между выстрелами.
    private float detectionRadius = 2.5f; // Радиус "видимости" шариков.

    private void Start()
    {
        // Получаем ссылку на стрелу (дочерний объект с индексом 0).
        arrow = transform.GetChild(0);
    }

    private void Update()
    {
        GameObject targetBall = FindClosestVisibleBall();

        if (targetBall != null)
        {
            BallController ballController = targetBall.GetComponent<BallController>();

            if (ballController != null && ballController.CanBeTargeted())
            {
                // Поворачиваем лук к цели.
                RotateParentToTarget(targetBall.transform);

                // Автоматический выстрел.
                shootTimer += Time.deltaTime;
                if (shootTimer >= shootInterval && arrow.GetComponent<ArrowController>().IsFlying == false)
                {
                    arrow.GetComponent<ArrowController>().Shoot(targetBall.transform);
                    shootTimer = 0f; // Сбрасываем таймер.
                }
            }
        }
    }

    private void RotateParentToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.parent.position).normalized;

        // Рассчитываем угол для оси Z.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0f, 0f, angle + 180f); // Учитываем, что изначально лук смотрит влево.
    }

    private GameObject FindClosestVisibleBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject closestBall = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(transform.position, ball.transform.position);

            Renderer renderer = ball.GetComponent<Renderer>();
            if (renderer != null && renderer.isVisible && distance <= detectionRadius) // Проверяем видимость и расстояние.
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBall = ball;
                }
            }
        }

        return closestBall;
    }

    //private void OnDrawGizmos()
    //{
    //    // Устанавливаем цвет для визуализации.
    //    Gizmos.color = Color.green;

    //    // Рисуем сферу вокруг лука с радиусом "видимости".
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //}

}
