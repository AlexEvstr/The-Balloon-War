using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int lives = 3;
    public int coins = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            Debug.Log("Game Over");
            // Реализовать экран окончания игры.
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        Debug.Log($"Coins: {coins}");
    }
}
