using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // Префаб шарика (один для всех уровней).
    public Material[] materials; // Материалы для шариков.
    public float spawnInterval = 0.5f; // Интервал между спавнами шариков.

    public IEnumerator SpawnBalls(int level)
    {
        // Определяем количество шариков для текущего уровня.
        int ballCount = 3 + level - 1;

        for (int i = 0; i < ballCount; i++)
        {
            // Создаём шарик.
            GameObject ball = Instantiate(ballPrefab, ballPrefab.transform.position, ballPrefab.transform.rotation);

            // Определяем материал в зависимости от уровня.
            int materialIndex = Mathf.Clamp(level - 1, 0, materials.Length - 1);
            ball.GetComponent<Renderer>().material = materials[materialIndex];

            // Привязываем события.
            BallController ballController = ball.GetComponent<BallController>();
            ballController.OnDestroyed = FindObjectOfType<GameManager>().OnBallDestroyed;
            ballController.OnEscaped = FindObjectOfType<GameManager>().OnBallEscaped;

            // Ждём перед спавном следующего шарика.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
