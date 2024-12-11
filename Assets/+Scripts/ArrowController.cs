using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f; // Скорость полёта стрелы.
    private Vector3 initialLocalPosition; // Исходная локальная позиция стрелы.
    private Quaternion initialLocalRotation; // Исходное локальное вращение стрелы.
    private Transform target; // Цель стрелы.
    public bool IsFlying { get; private set; } = false; // Флаг полёта.

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
        }
    }

    private void Update()
    {
        if (IsFlying)
        {
            if (target == null)
            {
                // Если цель уничтожена, возвращаем стрелу.
                ResetArrow();
                return;
            }

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
        target = null; // Сбрасываем цель.
        transform.localPosition = initialLocalPosition; // Возвращаем позицию.
        transform.localRotation = initialLocalRotation; // Возвращаем вращение.
    }
}
