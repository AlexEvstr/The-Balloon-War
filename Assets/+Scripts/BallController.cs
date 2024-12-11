using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 2f; // Скорость движения шарика.
    public int health = 1;  // Количество попаданий до уничтожения.

    private void Update()
    {
        // Движение шарика вверх.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Проверка на достижение верхней границы экрана.
        if (transform.position.y > Camera.main.orthographicSize + 1f)
        {
            GameManager.Instance.LoseLife(); // Уменьшаем жизни.
            Destroy(gameObject); // Удаляем шарик.
        }
    }

    public void Hit()
    {
        health--;

        if (health <= 0)
        {
            GameManager.Instance.AddCoins(10); // Добавляем монеты за уничтожение.
            Destroy(gameObject);
        }
    }
}
