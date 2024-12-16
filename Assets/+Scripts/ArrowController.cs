using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;
    private Transform target;
    public bool IsFlying { get; private set; } = false;
    private float maxFlightTime = 1f;
    private float flightTimer = 0f;

    private void Start()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    public void Shoot(Transform targetTransform)
    {
        if (!IsFlying)
        {
            IsFlying = true;
            target = targetTransform;

            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    private void Update()
    {
        flightTimer += Time.deltaTime;
        if (IsFlying && target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, target.position) < 0.05f)
            {
                BallController ball = target.GetComponent<BallController>();
                if (ball != null) ball.Hit();

                ResetArrow();
            }   
        }
        else
        {
            if (flightTimer >= maxFlightTime)
            {
                ResetArrow();
            }
        }
    }

    private void ResetArrow()
    {
        IsFlying = false;
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
        flightTimer = 0f;
    }
}