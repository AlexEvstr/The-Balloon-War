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
        int baseBallCount = Mathf.Min(3 + (level / 2), 50); // Максимум 10 шариков.

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

            // Динамическое время между спавнами (ускорение на высоких уровнях).
            float adjustedInterval = Mathf.Max(spawnInterval - (level * 0.05f), 0.3f);
            yield return new WaitForSeconds(adjustedInterval);
        }
    }


    private int GetMaterialIndexForBall(int ballIndex, int level)
    {
        int maxMaterialIndex = Mathf.Min(level - 1, materials.Length - 1); // Максимальный индекс материала для текущего уровня.

        // Постепенное распределение шариков по сложности.
        if (level == 1)
        {
            return 0; // Только красные шарики.
        }
        else if (level == 2)
        {
            return ballIndex < 3 ? 0 : 1; // 3 красных, 1 зелёный.
        }
        else if (level == 3)
        {
            if (ballIndex < 3) return 0; // 3 красных.
            if (ballIndex < 5) return 1; // 2 зелёных.
            return 2; // 1 синий.
        }
        else
        {
            // На высоких уровнях равномерно распределяем сложности.
            if (ballIndex % 3 == 0) return Mathf.Min(0, maxMaterialIndex); // Простые (красные).
            if (ballIndex % 3 == 1) return Mathf.Min(1, maxMaterialIndex); // Средние (зелёные).
            return maxMaterialIndex; // Сложные (синие и выше).
        }
    }


}
