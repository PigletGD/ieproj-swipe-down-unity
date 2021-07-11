using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestScroll : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float setScrollSpeed = -50.0f;

    public bool holdingTP = false;
    public bool onScrollArea = false;
    public Vector3 initialClickPos = Vector3.zero;
    public Vector3 currentClickPos = Vector3.zero;
    public Camera camMain;

    public float ScrollSpeed { get; private set; }
    
    private void Awake()
    {
        camMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ScrollSpeed = 0;
    }

    private void Update()
    {
        if (holdingTP)
        {
            initialClickPos = currentClickPos;
            currentClickPos = camMain.ScreenToWorldPoint(Input.mousePosition); 
        }

        CheckScroll();

        UpdateScrollSpeed();
    }

    public void CheckScroll()
    {
        if (onScrollArea)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
            {
                ScrollSpeed = setScrollSpeed;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
            {
                ScrollSpeed = 0.5f;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holdingTP = true;

        initialClickPos = camMain.ScreenToWorldPoint(Input.mousePosition);
        currentClickPos = initialClickPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holdingTP = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onScrollArea = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onScrollArea = false;
    }

    public void UpdateScrollSpeed()
    {
        if (holdingTP) ScrollSpeed = (currentClickPos.y - initialClickPos.y) * 100;
        else ScrollSpeed *= 0.95f;

        if (ScrollSpeed < -100f) ScrollSpeed = -100f;
        if (ScrollSpeed > 0.5f) ScrollSpeed = 0.5f;
    }
}
