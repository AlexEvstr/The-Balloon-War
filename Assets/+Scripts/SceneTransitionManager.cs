using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage; // Ссылка на UI Image для затемнения.
    public float fadeDuration = 1f; // Длительность затухания.

    private void Start()
    {
        Time.timeScale = 1;
        // Устанавливаем начальное состояние альфа-канала в 1 (полностью черный экран).
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        StartCoroutine(FadeIn()); // Запускаем плавное появление сцены.
    }

    public void LoadSceneWithFade(string sceneName)
    {
        // Запускаем корутину для загрузки сцены с затуханием.
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Плавно уменьшаем альфа-канал.
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 0f); // Убеждаемся, что экран полностью прозрачен.
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        Time.timeScale = 1;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Плавно увеличиваем альфа-канал.
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, 1f); // Убеждаемся, что экран полностью черный.

        SceneManager.LoadScene(sceneName); // Загружаем указанную сцену.
    }
}
