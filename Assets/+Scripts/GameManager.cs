using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public GameUIManager gameUIManager;

    public int lives = 3;
    private int currentLevel;
    private int totalBalls;
    private int coins;

    private void Start()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 1);
        coins = 0;

        gameUIManager.UpdateLevel(currentLevel);
        gameUIManager.UpdateLives(lives);
        gameUIManager.UpdateCoins(coins);

        StartCoroutine(SpawnBallsForLevel());
    }

    private IEnumerator SpawnBallsForLevel()
    {
        totalBalls = ballSpawner.GetTotalBallsForLevel(currentLevel);
        yield return StartCoroutine(ballSpawner.SpawnBalls(currentLevel));
    }

    public void OnBallDestroyed()
    {
        coins += 10; // Добавляем монеты за уничтожение шарика.
        gameUIManager.UpdateCoins(coins);

        totalBalls--;

        if (totalBalls <= 0 && lives > 0)
        {
            LevelComplete();
        }
    }

    public void OnBallEscaped()
    {
        lives--;
        gameUIManager.UpdateLives(lives);

        if (lives <= 0)
        {
            GameOver();
        }
    }

    private void LevelComplete()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);

        gameUIManager.UpdateLevel(currentLevel);

        gameUIManager.ShowVictoryWindow();
    }

    private void GameOver()
    {
        gameUIManager.ShowDefeatWindow();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
