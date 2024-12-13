using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Loading")
            StartCoroutine(OpenMenuScene());
    }

    private IEnumerator OpenMenuScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 1f);
    }
}