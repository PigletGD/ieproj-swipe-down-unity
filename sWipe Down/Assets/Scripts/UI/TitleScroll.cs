using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 initialClickPos = Vector3.zero;
    public Vector3 currentClickPos = Vector3.zero;
    public Camera camMain;
    public RectTransform rectTransform;

    private bool isDragging = false;
    private bool thresholdMet = false;
    private Vector3 startingPos = Vector3.zero;
    private Vector3 endingPos = Vector3.zero;

    public float ScrollSpeed { get; private set; }

    private void Awake()
    {
        camMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ScrollSpeed = 0;

        startingPos = rectTransform.position;
        endingPos = startingPos + new Vector3(0f, -1000f, 0f);
    }

    private void Update()
    {
        if (isDragging)
        {
            initialClickPos = currentClickPos;
            currentClickPos = Input.mousePosition;
        }

        if(!thresholdMet) UpdateScrollSpeed();
        MoveTitle();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        initialClickPos = Input.mousePosition;
        currentClickPos = initialClickPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        Debug.Log(rectTransform.anchoredPosition.y);

        if (rectTransform.anchoredPosition.y <= -180)
        {
            thresholdMet = true;
            ScrollSpeed = -25f;
        }
        else if (ScrollSpeed <= -12f)
        {
            thresholdMet = true;
            if (ScrollSpeed > -20f) ScrollSpeed = -25f;
        }
    }

    public void UpdateScrollSpeed()
    {
        if (isDragging) ScrollSpeed = (currentClickPos.y - initialClickPos.y);
        else ScrollSpeed *= 0.95f;
    }

    private void MoveTitle()
    {
        rectTransform.position += new Vector3(0f, ScrollSpeed, 0f);
        if(!thresholdMet) ScrollSpeed *= 0.8f;

        if (!isDragging && !thresholdMet)
        {
            Vector3 lerpingValue = Vector3.Lerp(rectTransform.position, startingPos, 0.01f);
            rectTransform.position = lerpingValue;
        }

        if(rectTransform.position.y < endingPos.y)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
