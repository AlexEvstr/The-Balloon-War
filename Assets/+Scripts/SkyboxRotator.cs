using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    void Update()
    {
        float rotation = RenderSettings.skybox.GetFloat("_Rotation");

        rotation += rotationSpeed * Time.deltaTime;

        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}