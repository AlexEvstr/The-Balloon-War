using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public WindowAnimator victoryWindow;
    public WindowAnimator defeatWindow;
    public WindowAnimator pauseWindow;
    public GameObject _nextBtn;

    public Text levelText;
    public Text coinsText;

    public List<Image> lifeImages;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void ShowVictoryWindow()
    {
        StartCoroutine(WaitForWindow(victoryWindow));
    }

    public void ShowDefeatWindow()
    {
        StartCoroutine(WaitForWindow(defeatWindow));
    }

    private IEnumerator WaitForWindow(WindowAnimator windowAnimator)
    {
        yield return new WaitForSeconds(1.0f);

        if (!windowAnimator.gameObject.activeInHierarchy)
        {
            windowAnimator.gameObject.SetActive(true);
            windowAnimator.AnimateOpen();
        }
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
        if (level >= 30)
        {
            _nextBtn.SetActive(false);
        }
    }

    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }
}