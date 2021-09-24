using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    private GameObject currentButtonGroup = null;
    private GameObject currentButtons = null;

    [SerializeField] private GameObject attackButtonGroup = default;
    [SerializeField] private GameObject defenseButtonGroup = default;
    [SerializeField] private GameObject supportButtonGroup = default;

    [SerializeField] private GameObject attackButtons = default;
    [SerializeField] private GameObject defenseButtons = default;
    [SerializeField] private GameObject supportButtons = default;

    [SerializeField] private VoidEvent validateButtons = default;

    private void Start()
    {
        attackButtonGroup.SetActive(false);
        defenseButtonGroup.SetActive(false);
        supportButtonGroup.SetActive(false);

        attackButtons.SetActive(false);
        defenseButtons.SetActive(false);
        supportButtons.SetActive(false);

        SwitchTab(0);
    }

    public void SwitchTab(int tab)
    {
        if (currentButtonGroup != null) currentButtonGroup.SetActive(false);
        if (currentButtons != null) currentButtons.SetActive(false);

        switch (tab)
        {
            case 0:
                currentButtonGroup = attackButtonGroup;
                currentButtons = attackButtons;
                break;
            case 1:
                currentButtonGroup = defenseButtonGroup;
                currentButtons = defenseButtons;
                break;
            case 2:
                currentButtonGroup = supportButtonGroup;
                currentButtons = supportButtons;
                break;
        }

        if (currentButtonGroup != null) currentButtonGroup.SetActive(true);
        if (currentButtons != null) currentButtons.SetActive(true);

        validateButtons.Raise();
    }

    public void PlaceBuildingInMenu()
    {
        if (buttons.Count <= 0) return;

        buttons[0].SetActive(true);

        buttons.RemoveAt(0);
    }
}