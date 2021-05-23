﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleToggle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Vector2 toggleOnPosition = Vector3.zero;
    [SerializeField] Vector2 toggleOffPosition = Vector3.zero;

    [SerializeField] RectTransform parent = null;
    [SerializeField] RectTransform rectTransform = null;

    public bool toggleOn = true;

    // Start is called before the first frame update
    void Start()
    {
        toggleOnPosition = parent.anchoredPosition;
        toggleOffPosition = new Vector2(parent.anchoredPosition.x - rectTransform.rect.width, parent.anchoredPosition.y);
        //Debug.Log(parent.anchoredPosition.x - rectTransform.rect.width);
    }

    // Update is called once per frame
    void Update()
    {
        float lerp;

        //Debug.Log(parent.position.x);

        if (toggleOn) lerp = Mathf.Lerp(parent.anchoredPosition.x, toggleOnPosition.x, 0.1f);
        else lerp = Mathf.Lerp(parent.anchoredPosition.x, toggleOffPosition.x, 0.1f);

        parent.anchoredPosition = new Vector2(lerp, parent.anchoredPosition.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        toggleOn = !toggleOn;
    }
}