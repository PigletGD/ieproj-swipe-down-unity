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
    [SerializeField] Image frontBorder;
    [SerializeField] Image backBorder;
    [SerializeField] Button button;

    private GameManager gameManager;

    private Color normal;
    private Color transparent;

    // Start is called before the first frame update
    void Start()
    {
        cost.text = building.buildingCost.ToString();
        icon.sprite = building.buildingIcon;

        gameManager = FindObjectOfType<GameManager>();

        button.interactable = false;

        normal = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        transparent = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        CheckButtonValidity();
    }

    public void CheckButtonValidity()
    {
        if (gameManager == null) gameManager = FindObjectOfType<GameManager>();

        if (gameManager.Currency < building.buildingCost)
        {
            backBorder.color = transparent;
            icon.color = transparent;
            frontBorder.color = transparent;

            button.interactable = false;
        }
        else
        {
            backBorder.color = normal;
            icon.color = normal;
            frontBorder.color = normal;

            button.interactable = true;
        }
    }
}