using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private BallSpawner ballSpawner; // Ссылка на спавнер шариков.
    private GameUIManager gameUIManager; // Ссылка на менеджер UI.

    public int lives = 3; // Количество жизней.
    private int currentLevel; // Текущий уровень.
    private int totalBalls; // Общее количество шариков в уровне.

    private void Start()
    {
        ballSpawner = GetComponent<BallSpawner>();
        gameUIManager = GetComponent<GameUIManager>();
        // Загружаем уровень из PlayerPrefs или устанавливаем первый уровень.
        currentLevel = PlayerPrefs.GetInt("Level", 1);

        // Определяем количество шариков.
        totalBalls = 3 + currentLevel - 1;

        // Запускаем спавн шариков.
        StartCoroutine(ballSpawner.SpawnBalls(currentLevel));
    }

    public void OnBallDestroyed()
    {
        totalBalls--;
        Debug.Log(totalBalls);
        // Проверяем, если все шарики уничтожены и остались жизни.
        if (totalBalls <= 0 && lives > 0)
        {
            LevelComplete();
        }
    }


    public void OnBallEscaped()
    {
        Debug.Log($"lives: {lives}-1 = {lives-1}");
        totalBalls--;
        lives--;
        // Если жизни закончились.
        if (lives <= 0)
        {
            GameOver();
        }
        if (totalBalls <= 0 && lives > 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        // Увеличиваем уровень.
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel); // Сохраняем уровень.

        // Открываем окно победы.
        gameUIManager.ShowVictoryWindow();
    }

    private void GameOver()
    {
        // Открываем окно поражения.
        gameUIManager.ShowDefeatWindow();
    }

    public void RestartScene()
    {
        // Перезагружаем текущую сцену.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
