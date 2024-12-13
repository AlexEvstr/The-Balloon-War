using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    public Button[] levelButtons; // Список кнопок уровней.
    private int bestLevel; // Лучший уровень.
    private SceneTransitionManager _sceneTransitionManager;

    private void Start()
    {
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        bestLevel = PlayerPrefs.GetInt("BestLevel", 1); // Получаем лучший уровень из PlayerPrefs.
        SetupLevelButtons();
    }

    private void SetupLevelButtons()
    {
        foreach (Button button in levelButtons)
        {
            // Получаем имя кнопки (номер уровня).
            string levelName = button.gameObject.name;

            // Парсим имя в число.
            if (int.TryParse(levelName, out int levelNumber))
            {
                if (levelNumber > bestLevel)
                {
                    // Если уровень больше лучшего, блокируем кнопку и включаем замок.
                    button.interactable = false;
                    button.transform.GetChild(1).gameObject.SetActive(true); // Включаем замок (дочерний объект с индексом 1).
                }
                else
                {
                    // Если уровень доступен, отключаем замок.
                    button.interactable = true;
                    button.transform.GetChild(1).gameObject.SetActive(false);
                }

                // Добавляем обработчик клика для доступных кнопок.
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnLevelButtonClicked(levelName));
            }
        }
    }

    private void OnLevelButtonClicked(string levelName)
    {
        PlayerPrefs.SetInt("Level", int.Parse(levelName)); // Сохраняем выбранный уровень в PlayerPrefs.
        PlayerPrefs.Save();
        _sceneTransitionManager.LoadSceneWithFade("Gameplay");
        
    }
}
