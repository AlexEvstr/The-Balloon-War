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

    private GameAudio _gameAudio;

    public BowUpgradeManager[] upgradeManagers; // Список всех апгрейдов луков.


    public int Coins
    {
        get => coins;
        private set
        {
            coins = value;
            PlayerPrefs.SetInt("Coins", coins); // Сохраняем монеты.
            gameUIManager.UpdateCoins(coins); // Обновляем интерфейс.

            // Обновляем все кнопки апгрейдов.
            foreach (var manager in upgradeManagers)
            {
                manager.UpdateButton();
            }
        }
    }


    private void Start()
    {
        _gameAudio = GetComponent<GameAudio>();
        currentLevel = PlayerPrefs.GetInt("Level", 1);
        Coins = PlayerPrefs.GetInt("Coins", 0); // Загружаем сохранённые монеты или устанавливаем 0.

        gameUIManager.UpdateLevel(currentLevel);
        gameUIManager.UpdateLives(lives);

        StartCoroutine(SpawnBallsForLevel());
    }

    private IEnumerator SpawnBallsForLevel()
    {
        totalBalls = ballSpawner.GetTotalBallsForLevel(currentLevel);
        yield return StartCoroutine(ballSpawner.SpawnBalls(currentLevel));
    }

    public void OnBallDestroyed()
    {
        Coins += 10; // Добавляем монеты за уничтожение шарика.

        totalBalls--;
        _gameAudio.PlayBoomSound();
        _gameAudio.PlayCoinsSound();

        if (totalBalls <= 0 && lives > 0)
        {
            LevelComplete();
        }
    }

    public void OnBallEscaped()
    {
        _gameAudio.PlayMinusHeartSound();
        lives--;
        totalBalls--;
        gameUIManager.UpdateLives(lives);

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
        _gameAudio.StopAllSounds();
        _gameAudio.PlayWinSound();
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);

        // Проверяем, если текущий уровень превышает лучший уровень.
        int bestLevel = PlayerPrefs.GetInt("BestLevel", 1);
        if (currentLevel > bestLevel)
        {
            PlayerPrefs.SetInt("BestLevel", currentLevel);
        }

        // Обновляем интерфейс, если требуется.
        //gameUIManager.UpdateLevel(currentLevel);

        gameUIManager.ShowVictoryWindow();
    }

    private void GameOver()
    {
        _gameAudio.StopAllSounds();
        _gameAudio.PlayLoseSound();
        gameUIManager.ShowDefeatWindow();
    }

    public void SpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount; // Снимаем монеты.
            _gameAudio.PlayUpgradeSound();
        }
    }

    public bool CanAfford(int amount)
    {
        return Coins >= amount; // Проверка, достаточно ли монет.
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
