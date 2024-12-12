using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // Скорость вращения

    void Update()
    {
        // Получаем текущий угол поворота Skybox
        float rotation = RenderSettings.skybox.GetFloat("_Rotation");

        // Увеличиваем угол поворота в зависимости от скорости и времени
        rotation += rotationSpeed * Time.deltaTime;

        // Устанавливаем новый угол поворота Skybox
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}
