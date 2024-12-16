using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameUIManager gameUIManager;

    public int lives = 3;
    private int currentLevel;
    private int totalBalls;
    private int coins;

    private GameAudio _gameAudio;

    public BowUpgradeManager[] upgradeManagers;

    private LevelManager _levelManager;

    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _winWindow;


    public int Coins
    {
        get => coins;
        private set
        {
            coins = value;
            PlayerPrefs.SetInt("Coins", coins);
            gameUIManager.UpdateCoins(coins);

            foreach (var manager in upgradeManagers)
            {
                manager.UpdateButton();
            }
        }
    }


    private void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        _gameAudio = GetComponent<GameAudio>();
        currentLevel = PlayerPrefs.GetInt("Level", 1);
        Coins = PlayerPrefs.GetInt("Coins", 0);

        gameUIManager.UpdateLevel(currentLevel);
        gameUIManager.UpdateLives(lives);

        StartCoroutine(SpawnBallsForLevel());
    }

    private IEnumerator SpawnBallsForLevel()
    {
        totalBalls = _levelManager.levels[currentLevel - 1].ballSequence.Count;
        yield return StartCoroutine(_levelManager.SpawnBalls(currentLevel - 1));
    }

    public void OnBallDestroyed()
    {
        Coins += 10;

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
        if (_loseWindow.activeInHierarchy || _winWindow.activeInHierarchy) return;
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
        _gameAudio.PlayWinSound();
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);

        int bestLevel = PlayerPrefs.GetInt("BestLevel", 1);
        if (currentLevel > bestLevel)
        {
            PlayerPrefs.SetInt("BestLevel", currentLevel);
        }

        gameUIManager.ShowVictoryWindow();
    }

    private void GameOver()
    {
        _gameAudio.PlayLoseSound();
        gameUIManager.ShowDefeatWindow();
    }

    public void SpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            _gameAudio.PlayUpgradeSound();
        }
    }

    public bool CanAfford(int amount)
    {
        return Coins >= amount;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}