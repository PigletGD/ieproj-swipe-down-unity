using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour
{
    public static float TAB_SIZE = 200;

    [SerializeField] Vector2 toggleOnPosition = Vector3.zero;
    [SerializeField] Vector2 toggleOffPosition = Vector3.zero;

    [SerializeField] RectTransform panel = null;

    public TabManager tabManager;

    public bool toggleOn = true;

    void Start()
    {
        toggleOnPosition = new Vector2(0, 0);
        toggleOffPosition = new Vector2(0, -TAB_SIZE);
    }

    // Update is called once per frame
    void Update()
    {
        float lerp;

        //Debug.Log(panel.position.x);

        if (toggleOn) lerp = Mathf.Lerp(panel.anchoredPosition.y, toggleOnPosition.y, 0.1f);
        else lerp = Mathf.Lerp(panel.anchoredPosition.y, toggleOffPosition.y, 0.1f);

        panel.anchoredPosition = new Vector2(panel.anchoredPosition.x, lerp);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        tabManager.ActivateTab();
        toggleOn = !toggleOn;
    }

    public void OnButtonPressed()
    {
        if(toggleOn)
        {
            tabManager.HideAllTabs();
        }
        else
        {
            tabManager.ActivateTab();
            toggleOn = !toggleOn;
        }

        tabManager.lastActiveTab = this;
    }
}
