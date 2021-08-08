using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] Transform buttonHolder;

    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            PlaceUpgradeInMenu();
    }

    public void PlaceUpgradeInMenu()
    {
        if (buttons.Count <= 0) return;

        Instantiate(buttons[0], buttonHolder);

        buttons.RemoveAt(0);
    }
}