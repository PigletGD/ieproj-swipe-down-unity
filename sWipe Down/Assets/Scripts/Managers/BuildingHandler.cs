using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    public void PlaceBuildingInMenu()
    {
        if (buttons.Count <= 0) return;

        buttons[0].SetActive(true);

        buttons.RemoveAt(0);
    }
}