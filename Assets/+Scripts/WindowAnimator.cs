using System.Collections;
using UnityEngine;

public class WindowAnimator : MonoBehaviour
{
    private Transform childObject;
    public float animationDuration = 0.5f;

    private void Awake()
    {
        childObject = transform.GetChild(0);
        childObject.localScale = Vector3.zero;
    }

    public void AnimateOpen()
    {
        StartCoroutine(ScaleOverTime(Vector3.zero, Vector3.one, animationDuration));
    }

    public void AnimateClose()
    {
        StartCoroutine(ScaleOverTime(Vector3.one, Vector3.zero, animationDuration, () => gameObject.SetActive(false)));
    }

    private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration, System.Action onComplete = null)
    {
        float elapsedTime = 0f;
        childObject.localScale = startScale;

        while (elapsedTime < duration)
        {
            childObject.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        childObject.localScale = endScale;
        onComplete?.Invoke();
    }
}