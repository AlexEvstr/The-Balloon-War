using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public float SpawnInterval;
    public List<GameObject> ballSequence;
}

public class LevelManager : MonoBehaviour
{
    public List<LevelData> levels;

    public IEnumerator SpawnBalls(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            Debug.LogError("Level index out of range!");
            yield break;
        }

        LevelData currentLevel = levels[levelIndex];

        foreach (GameObject ballPrefab in currentLevel.ballSequence)
        {
            GameObject ball = Instantiate(ballPrefab, ballPrefab.transform.position, ballPrefab.transform.rotation);
            BallController ballController = ball.GetComponent<BallController>();
            
            GameManager gameManager = FindObjectOfType<GameManager>();
            ballController.OnDestroyed = gameManager.OnBallDestroyed;
            ballController.OnEscaped = gameManager.OnBallEscaped;

            yield return new WaitForSeconds(currentLevel.SpawnInterval);
        }
    }

    public int GetTotalLevels()
    {
        return levels.Count;
    }
}