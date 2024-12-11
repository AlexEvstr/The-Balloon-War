using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 2f; // Скорость движения шарика.
    public System.Action OnDestroyed; // Событие уничтожения шарика.
    public System.Action OnEscaped; // Событие выхода за верхнюю границу.

    private void Update()
    {
        // Двигаем шарик вверх.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Проверяем, если шарик выше 6.5 по Y.
        if (transform.position.y > 6.5f)
        {
            OnEscaped?.Invoke(); // Отнимаем жизнь.
            Destroy(gameObject); // Уничтожаем шарик.
        }
    }

    public bool CanBeTargeted()
    {
        // Возвращаем true, если шарик выше -4.3 по Y.
        return transform.position.y >= -4.3f;
    }

    public void Hit()
    {
        // Логика уничтожения шарика.
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
