using UnityEngine;
using UnityEngine.UI;

public class BowUpgradeManager : MonoBehaviour
{
    public BowController bow; // Ссылка на лук.
    private Button upgradeButton; // Кнопка апгрейда.
    public Text costText; // Текст с ценой апгрейда.

    public int upgradeCost = 50; // Стоимость первого апгрейда.
    public int costIncrease = 20; // Увеличение стоимости после каждого апгрейда.
    public float fireRateIncrease = 0.1f; // Увеличение скорости стрельбы после апгрейда.
    private string playerPrefsKey;

    private GameManager gameManager;
    private const float minShootInterval = 0.3f; // Минимальная скорость стрельбы.

    private void Start()
    {
        if (bow.GetShootInterval() <= 0.3)
        {
            costText.text = "MAX";
            upgradeButton.interactable = false;
        }
        gameManager = FindObjectOfType<GameManager>();

        upgradeButton = GetComponent<Button>();

        // Уникальный ключ для стоимости этой кнопки.
        playerPrefsKey = "UpgradeCost_" + gameObject.name;

        // Загружаем сохранённую стоимость или устанавливаем начальную.
        upgradeCost = PlayerPrefs.GetInt(playerPrefsKey, upgradeCost);
        UpdateButton();
    }

    public void TryUpgrade()
    {
        if (gameManager.CanAfford(upgradeCost) && bow.GetShootInterval() > minShootInterval)
        {
            // Снимаем монеты через GameManager.
            gameManager.SpendCoins(upgradeCost);

            // Улучшаем лук.
            bow.SetShootInterval(Mathf.Max(minShootInterval, bow.GetShootInterval() - fireRateIncrease));
            bow.SaveShootInterval(); // Сохраняем изменения.

            // Увеличиваем стоимость следующего апгрейда.
            upgradeCost += costIncrease;

            SaveUpgradeCost();

            // Обновляем интерфейс.
            UpdateButton();
        }
    }

    public void UpdateButton()
    {
        // Проверяем, хватает ли монет для апгрейда.
        if (gameManager.CanAfford(upgradeCost))
        {
            upgradeButton.interactable = true; // Включаем кнопку.
        }
        else
        {
            upgradeButton.interactable = false; // Выключаем кнопку.
        }

        // Обновляем текст стоимости или пишем "MAX", если апгрейд больше невозможен.
        if (bow.GetShootInterval() <= 0.3f)
        {
            costText.text = "MAX";
            upgradeButton.interactable = false; // Отключаем кнопку.
        }
        else
        {
            costText.text = upgradeCost.ToString();
        }
    }

    private void SaveUpgradeCost()
    {
        PlayerPrefs.SetInt(playerPrefsKey, upgradeCost);
        PlayerPrefs.Save();
    }
}
