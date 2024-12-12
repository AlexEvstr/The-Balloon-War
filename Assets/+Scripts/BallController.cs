using UnityEngine;

public class BallController : MonoBehaviour
{
    public Material[] materials; // Массив материалов для шарика (по уровням сложности).
    public float speed = 2f; // Скорость движения шарика.
    public System.Action OnDestroyed; // Событие уничтожения шарика.
    public System.Action OnEscaped; // Событие выхода за верхнюю границу экрана.

    private int currentLevel; // Текущий уровень прочности шарика.

    private GameObject _defeatPanel;

    private void Start()
    {
        // Изначально шарик получает максимальный уровень сложности.
        currentLevel = materials.Length - 1;

        // Устанавливаем начальный материал.
        UpdateMaterial();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Проверяем, если шарик вышел за верхнюю границу экрана.
        if (transform.position.y > 6.5f)
        {
            OnEscaped?.Invoke(); // Отнимаем жизнь.
            Destroy(gameObject); // Уничтожаем шарик.
        }
    }

    public void Hit()
    {
        if (currentLevel > 0)
        {
            // Переход к следующему уровню прочности.
            currentLevel--;
            UpdateMaterial();
        }
        else
        {
            // Уничтожение шарика.
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }

    public bool CanBeTargeted()
    {
        // Возвращаем true, если шарик выше -4.3 по Y.
        return transform.position.y >= -4.3f;
    }

    private void UpdateMaterial()
    {
        // Обновляем материал шарика в зависимости от текущего уровня.
        if (currentLevel >= 0 && currentLevel < materials.Length)
        {
            GetComponent<Renderer>().material = materials[currentLevel];
        }
    }
}
