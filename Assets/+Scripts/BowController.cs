using UnityEngine;

public class BowController : MonoBehaviour
{
    private Transform arrow;
    private float shootInterval = 1.5f;
    private float shootTimer;
    private float detectionRadius = 2.5f;

    private string playerPrefsKey;
    private GameObject currentTarget;

    [SerializeField] private GameAudio _gameAudio;
    [SerializeField] private GameObject _loseWindow;
    [SerializeField] private GameObject _winWindow;

    private void OnEnable()
    {
        playerPrefsKey = "Bow_" + gameObject.name + "_ShootInterval";

        shootInterval = PlayerPrefs.GetFloat(playerPrefsKey, shootInterval);
        arrow = transform.GetChild(0);
    }

    private void Update()
    {
        if (currentTarget != null && currentTarget.GetComponent<BallController>().CanBeTargeted())
        {
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (distance > detectionRadius)
            {
                currentTarget = null;
                return;
            }

            if (!arrow.GetComponent<ArrowController>().IsFlying)
            {
                RotateParentToTarget(currentTarget.transform);
            }

            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval && arrow.GetComponent<ArrowController>().IsFlying == false)
            {
                arrow.GetComponent<ArrowController>().Shoot(currentTarget.transform);
                shootTimer = 0f;
                _gameAudio.PlayShootSound();
            }
        }
        else
        {
            currentTarget = FindClosestVisibleBall();
        }
    }


    public void SaveShootInterval()
    {
        PlayerPrefs.SetFloat(playerPrefsKey, shootInterval);
        PlayerPrefs.Save();
    }

    private void RotateParentToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.parent.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
    }

    private GameObject FindClosestVisibleBall()
    {
        if (_loseWindow.activeInHierarchy || _winWindow.activeInHierarchy) return null;

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject closestBall = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject ball in balls)
        {
            float distance = Vector3.Distance(transform.position, ball.transform.position);

            Renderer renderer = ball.GetComponent<Renderer>();
            if (renderer != null && renderer.isVisible && distance <= detectionRadius)
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestBall = ball;
                }
            }
        }

        return closestBall;
    }

    public float GetShootInterval()
    {
        return shootInterval;
    }

    public void SetShootInterval(float newInterval)
    {
        shootInterval = newInterval;
    }
}