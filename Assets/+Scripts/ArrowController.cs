using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f; // Скорость движения стрелы.

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Удаляем стрелу, если она выходит за границы экрана.
        if (Mathf.Abs(transform.position.x) > Camera.main.orthographicSize * 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BallController ball = other.GetComponent<BallController>();
            if (ball != null)
            {
                ball.Hit(); // Уменьшаем здоровье шарика.
                Destroy(gameObject); // Удаляем стрелу.
            }
        }
    }
}
