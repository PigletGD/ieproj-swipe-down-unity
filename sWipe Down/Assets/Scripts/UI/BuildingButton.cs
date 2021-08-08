using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] BuildingSO building;
    [SerializeField] Text cost;
    [SerializeField] Image icon;
    [SerializeField] Button button;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        cost.text = building.buildingCost.ToString();
        icon.sprite = building.buildingIcon;

        gameManager = FindObjectOfType<GameManager>();

        button.interactable = false;
    }

    public void CheckButtonValidity()
    {
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();

        if (gameManager.Currency < building.buildingCost) button.interactable = false;
        else button.interactable = true;
    }
}