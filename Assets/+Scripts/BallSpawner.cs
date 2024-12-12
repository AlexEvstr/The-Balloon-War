using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Префаб шарика.
    public Material[] materials; // Материалы для шариков (по уровням сложности).
    public float spawnInterval = 0.5f; // Интервал между спавнами шариков.

    //private int baseBallCount = 5; // Количество базовых (красных) шариков на первом уровне.

    public int GetTotalBallsForLevel(int level)
    {
        // Базовое количество шариков: растёт на 1 каждые 2 уровня.
        int baseBallCount = Mathf.Min(3 + (level / 2), 10); // Максимум 10 шариков.

        return baseBallCount;
    }

    public IEnumerator SpawnBalls(int level)
    {
        int totalBalls = GetTotalBallsForLevel(level);

        for (int i = 0; i < totalBalls; i++)
        {
            // Создаём шарик.
            GameObject ball = Instantiate(ballPrefab, ballPrefab.transform.position, ballPrefab.transform.rotation);

            // Настраиваем материал для шарика.
            int materialIndex = GetMaterialIndexForBall(i, level);
            Material[] ballMaterials = new Material[materialIndex + 1];

            for (int j = 0; j <= materialIndex; j++)
            {
                ballMaterials[j] = materials[j];
            }

            // Устанавливаем материалы для шарика.
            BallController ballController = ball.GetComponent<BallController>();
            ballController.materials = ballMaterials;

            // Привязываем события OnDestroyed и OnEscaped.
            GameManager gameManager = FindObjectOfType<GameManager>();
            ballController.OnDestroyed = gameManager.OnBallDestroyed;
            ballController.OnEscaped = gameManager.OnBallEscaped;

            // Ждём перед спавном следующего шарика.
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private int GetMaterialIndexForBall(int ballIndex, int level)
    {
        // Постепенное добавление сложных шариков.
        if (level <= 2)
        {
            return 0; // Только красные шарики.
        }
        else if (level <= 4)
        {
            return ballIndex < 3 ? 0 : 1; // Красные и немного зелёных.
        }
        else if (level <= 6)
        {
            return ballIndex < 2 ? 0 : (ballIndex < 5 ? 1 : 2); // Добавляем синие.
        }
        else
        {
            // Постепенное усложнение с добавлением большего количества сложных шариков.
            return Mathf.Min((level - 2) / 2, materials.Length - 1);
        }
    }
}
