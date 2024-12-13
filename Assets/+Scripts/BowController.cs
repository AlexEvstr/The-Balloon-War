using UnityEngine;

public class BowController : MonoBehaviour
{
    private Transform arrow; // Ссылка на стрелу (первый дочерний объект).
    private float shootInterval = 1.5f; // Интервал между выстрелами.
    private float shootTimer; // Таймер для отслеживания времени между выстрелами.
    private float detectionRadius = 2.5f; // Радиус "видимости" шариков.

    private string playerPrefsKey;
    private GameObject currentTarget; // Текущая цель.

    private void OnEnable()
    {
        // Уникальный ключ для этого лука (можно использовать имя объекта).
        playerPrefsKey = "Bow_" + gameObject.name + "_ShootInterval";

        // Загружаем сохранённое значение или оставляем дефолтное.
        shootInterval = PlayerPrefs.GetFloat(playerPrefsKey, shootInterval);
        // Получаем ссылку на стрелу (дочерний объект с индексом 0).
        arrow = transform.GetChild(0);

        Debug.Log(shootInterval);
    }

    private void Update()
    {
        // Если текущая цель существует и доступна.
        if (currentTarget != null && currentTarget.GetComponent<BallController>().CanBeTargeted())
        {
            // Проверяем, остаётся ли цель в радиусе видимости.
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distance > detectionRadius)
            {
                currentTarget = null; // Убираем цель, если она вне радиуса.
                return;
            }

            if (!arrow.GetComponent<ArrowController>().IsFlying)
            {
                RotateParentToTarget(currentTarget.transform);
            }

            // Автоматический выстрел.
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval && arrow.GetComponent<ArrowController>().IsFlying == false)
            {
                arrow.GetComponent<ArrowController>().Shoot(currentTarget.transform);
                shootTimer = 0f; // Сбрасываем таймер.
            }
        }
        else
        {
            // Ищем новую цель, если текущая недоступна.
            currentTarget = FindClosestVisibleBall();
        }
    }


    public void SaveShootInterval()
    {
        PlayerPrefs.SetFloat(playerPrefsKey, shootInterval);
        PlayerPrefs.Save(); // Сохраняем изменения.
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

    public float GetShootInterval()
    {
        return shootInterval;
    }

    public void SetShootInterval(float newInterval)
    {
        shootInterval = newInterval;
    }
}
