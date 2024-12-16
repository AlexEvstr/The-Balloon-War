using UnityEngine;
using UnityEngine.UI;

public class BowUpgradeManager : MonoBehaviour
{
    public BowController bow;
    private Button upgradeButton;
    public Text costText;

    public int upgradeCost = 50;
    public int costIncrease = 20;
    public float fireRateIncrease = 0.1f;
    private string playerPrefsKey;

    private GameManager gameManager;
    private const float minShootInterval = 0.3f;

    private void Start()
    {
        if (bow.GetShootInterval() <= 0.3)
        {
            costText.text = "MAX";
            upgradeButton.interactable = false;
        }
        gameManager = FindObjectOfType<GameManager>();

        upgradeButton = GetComponent<Button>();

        playerPrefsKey = "UpgradeCost_" + gameObject.name;

        upgradeCost = PlayerPrefs.GetInt(playerPrefsKey, upgradeCost);
        UpdateButton();
    }

    public void TryUpgrade()
    {
        if (gameManager.CanAfford(upgradeCost) && bow.GetShootInterval() > minShootInterval)
        {
            gameManager.SpendCoins(upgradeCost);

            bow.SetShootInterval(Mathf.Max(minShootInterval, bow.GetShootInterval() - fireRateIncrease));
            bow.SaveShootInterval();

            upgradeCost += costIncrease;

            SaveUpgradeCost();

            UpdateButton();
        }
    }

    public void UpdateButton()
    {
        if (gameManager.CanAfford(upgradeCost))
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }

        if (bow.GetShootInterval() <= 0.3f)
        {
            costText.text = "MAX";
            upgradeButton.interactable = false;
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