using UnityEngine;

public class BallController : MonoBehaviour
{
    public Material[] materials;
    private float speed = 2f;
    public System.Action OnDestroyed;
    public System.Action OnEscaped;

    [SerializeField] private int currentLevel;

    private void Start()
    {
        UpdateMaterial();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.y > 6.5f)
        {
            OnEscaped?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        if (currentLevel > 0)
        {
            currentLevel--;
            UpdateMaterial();
        }
        else
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }

    public bool CanBeTargeted()
    {
        return transform.position.y >= -4.3f;
    }

    private void UpdateMaterial()
    {
        if (currentLevel >= 0 && currentLevel < materials.Length)
        {
            GetComponent<Renderer>().material = materials[currentLevel];
        }
    }
}