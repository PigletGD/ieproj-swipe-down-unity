using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPanelResizer : MonoBehaviour
{
    [SerializeField] float toggleOnPosition = 0;
    [SerializeField] float toggleOffPosition = 0;

    [SerializeField] RectTransform idlePanel = null;
    [SerializeField] RectTransform rectTransformPanel = null;
    [SerializeField] RectTransform rectTransformToggleButton = null;

    [SerializeField] IdleToggle idleToggle = null;

    public bool toggleOn = true;

    // Start is called before the first frame update
    void Start()
    {
        toggleOffPosition = rectTransformPanel.anchoredPosition.x;
        toggleOnPosition = rectTransformPanel.anchoredPosition.x + (idlePanel.rect.width / 2);
        //Debug.Log(parent.anchoredPosition.x - rectTransform.rect.width);

        rectTransformPanel.anchoredPosition = new Vector2(toggleOnPosition, rectTransformPanel.anchoredPosition.y);
        rectTransformToggleButton.anchoredPosition = new Vector2(toggleOnPosition, rectTransformToggleButton.anchoredPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        float lerpPanel;
        float lerpButton;

        //Debug.Log(parent.position.x);

        if (idleToggle.toggleOn) 
        {
            lerpPanel = Mathf.Lerp(rectTransformPanel.anchoredPosition.x, toggleOnPosition, 0.1f);
            lerpButton = Mathf.Lerp(rectTransformToggleButton.anchoredPosition.x, toggleOnPosition, 0.1f);
        }
        else
        {
            lerpPanel = Mathf.Lerp(rectTransformPanel.anchoredPosition.x, toggleOffPosition, 0.1f);
            lerpButton = Mathf.Lerp(rectTransformToggleButton.anchoredPosition.x, toggleOffPosition, 0.1f);
        }

        rectTransformPanel.anchoredPosition = new Vector2(lerpPanel, rectTransformPanel.anchoredPosition.y);
        rectTransformToggleButton.anchoredPosition = new Vector2(lerpButton, rectTransformToggleButton.anchoredPosition.y);
    }


}