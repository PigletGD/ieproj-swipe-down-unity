using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabManager : MonoBehaviour
{
    [SerializeField]private List<Tab> tabList;
    [SerializeField] private RectTransform buttonsHolder;

    public Tab lastActiveTab;

    [SerializeField]private bool active = true;
    [SerializeField]private Vector2 toggleOnPostion;
    [SerializeField]private Vector2 toggleOffPostion;

    // Start is called before the first frame update
    private void Start()
    {
        toggleOnPostion = buttonsHolder.transform.position;
        toggleOffPostion = buttonsHolder.transform.position;
        toggleOffPostion.y -= Tab.TAB_SIZE;
    }

    private void Update()
    {
        float lerp;

        if (active) lerp = Mathf.Lerp(buttonsHolder.anchoredPosition.y, 0, 0.1f);
        else lerp = Mathf.Lerp(buttonsHolder.anchoredPosition.y, -Tab.TAB_SIZE, 0.1f);

        //if (active) lerp = Mathf.Lerp(buttonsHolder.position.y, toggleOnPostion.y, 0.1f);
        //else lerp = Mathf.Lerp(buttonsHolder.position.y, toggleOffPostion.y, 0.1f);

        buttonsHolder.anchoredPosition = new Vector2(buttonsHolder.anchoredPosition.x, lerp);
    }

    public void HideAllTabs()
    {
        active = false;
        foreach (Tab tab in tabList)
        {
            tab.toggleOn = false;
        }
    }

    public void ActivateTab()
    {
        HideAllTabs();
        active = true;
    }
    public void OnButtonPress()
    {
        if(active)
        {
            HideAllTabs();
        }
        else
        {
            lastActiveTab.toggleOn = true;
            active = true;
        }
    }
}
