using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private IntEvent onCurrencySpent = default;
    [SerializeField] private IntEvent onUpgradeBought = default;

    [SerializeField] private string description = default;
    [SerializeField] public int price = 0;
    [SerializeField] public int value = 0;

    [SerializeField] private Button button = default;
    [SerializeField] private Text title = default;
    [SerializeField] private Text cost = default;
    
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        title.text = description;
        cost.text = price.ToString();

        CheckButtonValidity();
    }

    public void BuyUpgrade()
    {
        onCurrencySpent.Raise(price);
        onUpgradeBought.Raise(value);

        gameObject.SetActive(false);
    }

    public void CheckButtonValidity()
    {
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();

        if (gameManager.Currency < price) button.interactable = false;
        else button.interactable = true;
    }
}
