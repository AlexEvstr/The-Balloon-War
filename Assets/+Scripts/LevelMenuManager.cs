using UnityEngine;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    public Button[] levelButtons;
    private int bestLevel;
    private SceneTransitionManager _sceneTransitionManager;

    private void Start()
    {
        _sceneTransitionManager = GetComponent<SceneTransitionManager>();
        bestLevel = PlayerPrefs.GetInt("BestLevel", 1);
        SetupLevelButtons();
    }

    private void SetupLevelButtons()
    {
        foreach (Button button in levelButtons)
        {
            string levelName = button.gameObject.name;

            if (int.TryParse(levelName, out int levelNumber))
            {
                if (levelNumber > bestLevel)
                {
                    button.interactable = false;
                    button.transform.GetChild(1).gameObject.SetActive(true);
                }
                else
                {
                    button.interactable = true;
                    button.transform.GetChild(1).gameObject.SetActive(false);
                }

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => OnLevelButtonClicked(levelName));
            }
        }
    }

    private void OnLevelButtonClicked(string levelName)
    {
        PlayerPrefs.SetInt("Level", int.Parse(levelName));
        PlayerPrefs.Save();
        _sceneTransitionManager.LoadSceneWithFade("Gameplay");
        
    }
}