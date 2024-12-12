using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f; // Скорость полета стрелы.
    private Vector3 initialLocalPosition; // Исходная позиция стрелы.
    private Quaternion initialLocalRotation; // Исходное вращение стрелы.
    private Transform target; // Цель стрелы.
    public bool IsFlying { get; private set; } = false; // Флаг, летит ли стрела.

    private void Start()
    {
        // Сохраняем исходные параметры.
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    public void Shoot(Transform targetTransform)
    {
        if (!IsFlying)
        {
            IsFlying = true;
            target = targetTransform;

            // Поворачиваем стрелу к цели.
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void Update()
    {
        if (IsFlying && target != null)
        {
            // Двигаем стрелу к цели.
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Если достигли цели.
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                BallController ball = target.GetComponent<BallController>();
                if (ball != null) ball.Hit();

                ResetArrow(); // Возвращаем стрелу.
            }
        }
    }

    private void ResetArrow()
    {
        IsFlying = false;
        transform.localPosition = initialLocalPosition; // Возвращаем позицию.
        transform.localRotation = initialLocalRotation; // Возвращаем вращение.
    }
}
