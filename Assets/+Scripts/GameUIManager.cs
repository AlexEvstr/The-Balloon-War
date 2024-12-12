using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public WindowAnimator victoryWindow;
    public WindowAnimator defeatWindow;
    public WindowAnimator pauseWindow;

    // UI элементы для уровня и монет.
    public Text levelText;
    public Text coinsText;

    // Картинки для жизней.
    public List<Image> lifeImages;

    public void ShowVictoryWindow()
    {
        Time.timeScale = 0f;
        victoryWindow.gameObject.SetActive(true);
        victoryWindow.AnimateOpen();
    }

    public void ShowDefeatWindow()
    {
        Time.timeScale = 0f;
        defeatWindow.gameObject.SetActive(true);
        defeatWindow.AnimateOpen();
    }

    public void ShowPauseWindow()
    {
        Time.timeScale = 0f;
        pauseWindow.gameObject.SetActive(true);
        pauseWindow.AnimateOpen();
    }

    public void HidePauseWindow()
    {
        pauseWindow.AnimateClose();
        StartCoroutine(ResumeTimeAfterPause(pauseWindow.animationDuration));
    }

    private IEnumerator ResumeTimeAfterPause(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
    }

    // Методы для обновления UI.
    public void UpdateLives(int lives)
    {
        for (int i = 0; i < lifeImages.Count; i++)
        {
            lifeImages[i].enabled = i < lives;
        }
    }

    public void UpdateLevel(int level)
    {
        levelText.text = "Level: " + level;
    }

    public void UpdateCoins(int coins)
    {
        coinsText.text = "Coins: " + coins;
    }
}
