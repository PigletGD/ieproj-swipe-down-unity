using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] Transform buttonHolder;

    [ContextMenuItem("Organize Upgrades", "OrganizeUpgrades")]
    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    private void Start()
    {
        PlaceUpgradeInMenu();
        PlaceUpgradeInMenu();
        PlaceUpgradeInMenu();
    }

    public void PlaceUpgradeInMenu()
    {
        if (buttons.Count <= 0) return;

        Instantiate(buttons[0], buttonHolder);

        buttons.RemoveAt(0);
    }

    [ContextMenu("Organize Upgrades")]
    public void OrganizeUpgrades()
    {
        buttons = buttons.OrderBy(e => 
        {
            UpgradeButton UB = e.GetComponent<UpgradeButton>();
            return UB.price;
        }).ToList();

        Debug.Log("Finished Organizing Upgrades");
    }
}