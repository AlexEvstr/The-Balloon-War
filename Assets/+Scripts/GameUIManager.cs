using System.Collections;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public WindowAnimator victoryWindow;
    public WindowAnimator defeatWindow;
    public WindowAnimator pauseWindow;

    public void ShowVictoryWindow()
    {
        Time.timeScale = 0f; // Останавливаем время.
        victoryWindow.gameObject.SetActive(true);
        victoryWindow.AnimateOpen();
    }

    public void ShowDefeatWindow()
    {
        Time.timeScale = 0f; // Останавливаем время.
        defeatWindow.gameObject.SetActive(true);
        defeatWindow.AnimateOpen();
    }

    public void ShowPauseWindow()
    {
        Time.timeScale = 0f; // Останавливаем время.
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
        yield return new WaitForSecondsRealtime(delay); // Ждём реальное время.
        Time.timeScale = 1f; // Возобновляем время.
    }
}
