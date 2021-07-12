using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField]private List<Tab> tabList;
    [SerializeField] private RectTransform buttonsHolder;

    public Tab lastActiveTab;

    [ReadOnly]private bool active = true;
    [ReadOnly]private Vector2 toggleOnPostion;
    [ReadOnly] private Vector2 toggleOffPostion;

    // Start is called before the first frame update
    private void Start()
    {
        toggleOnPostion = buttonsHolder.transform.position;
        toggleOffPostion = toggleOnPostion;
        toggleOffPostion.y -= Tab.TAB_SIZE;
    }

    private void Update()
    {
        float lerp;

        if (active) lerp = Mathf.Lerp(buttonsHolder.anchoredPosition.y, 35, 0.1f);
        else lerp = Mathf.Lerp(buttonsHolder.anchoredPosition.y, -Tab.TAB_SIZE + 35, 0.1f);

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
