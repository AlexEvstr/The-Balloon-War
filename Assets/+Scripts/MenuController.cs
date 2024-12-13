using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private WindowAnimator _levelWindow;
    [SerializeField] private WindowAnimator _tutorialWindow;

    public void OpenLevels()
    {
        _levelWindow.gameObject.SetActive(true);
        _levelWindow.AnimateOpen();
    }

    public void CloseLevels()
    {
        _levelWindow.AnimateClose();
    }

    public void OpenTutorial()
    {
        _tutorialWindow.gameObject.SetActive(true);
        _tutorialWindow.AnimateOpen();
    }

    public void CloseTutorial()
    {
        _tutorialWindow.AnimateClose();
    }
}