using UnityEngine;

public class BowController : MonoBehaviour
{
    private Transform arrow; // Ссылка на стрелу (первый дочерний объект).
    public float shootInterval = 2f; // Интервал между выстрелами.
    private float shootTimer; // Таймер для отслеживания времени между выстрелами.

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
            // Поворачиваем родительский объект лука к ближайшему шарику.
            RotateParentToTarget(targetBall.transform);

            // Отсчитываем время для автоматического выстрела.
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootInterval && arrow.GetComponent<ArrowController>().IsFlying == false)
            {
                arrow.GetComponent<ArrowController>().Shoot(targetBall.transform);
                shootTimer = 0f; // Сбрасываем таймер.
            }
        }
    }

    private void RotateParentToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.parent.position).normalized;

        // Рассчитываем угол для оси Z.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0f, 0f, angle + 180f); // Учитываем, что изначально смотрим влево.
    }

    private GameObject FindClosestVisibleBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject closestBall = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject ball in balls)
        {
            Renderer ballRenderer = ball.GetComponent<Renderer>();
            if (ballRenderer != null && ballRenderer.isVisible) // Проверяем видимость.
            {
                float distance = Vector3.Distance(transform.parent.position, ball.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBall = ball;
                }
            }
        }

        return closestBall;
    }
}
