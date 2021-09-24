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

    float autoScrollSpeed = 0;
    public int goldPaperChance = 0;

    public float ScrollSpeed { get; private set; }

    private bool isScrolling = false;

    public float dragThreshold = -40.0f;

    private void Awake()
    {
        camMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ScrollSpeed = 0;
    }

    private void Update()
    {
        Application.targetFrameRate = 60;

        if (holdingTP)
        {
            initialClickPos = currentClickPos;
            currentClickPos = camMain.ScreenToWorldPoint(Input.mousePosition); 
        }

        //if (Input.GetKeyDown(KeyCode.K)) AddAutoScrollMultiplier(2);

        if (Time.timeScale <= 0.01f) return;

        CheckScroll();

        UpdateScrollSpeed();
    }

    public void CheckScroll()
    {
        isScrolling = false;

        if (onScrollArea)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
            {
                ScrollSpeed = setScrollSpeed;
                isScrolling = true;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
            {
                ScrollSpeed = 0.5f;
                isScrolling = true;
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
        if (holdingTP) {
            float strength = (currentClickPos.y - initialClickPos.y) * 100;

            if (strength > dragThreshold) ScrollSpeed = (strength / dragThreshold) * setScrollSpeed * 1.25f;
            else ScrollSpeed = setScrollSpeed * 1.25f;
        }
        else ScrollSpeed *= 0.95f;

        if (!isScrolling && autoScrollSpeed != 0)
        {
            if (Mathf.Abs(ScrollSpeed - autoScrollSpeed) > 0.001f)
                ScrollSpeed = Mathf.Lerp(ScrollSpeed, autoScrollSpeed, 0.05f);
            else ScrollSpeed = autoScrollSpeed;
        }

        if (ScrollSpeed < -300f) ScrollSpeed = -300f;
        else if (ScrollSpeed > 0.5f) ScrollSpeed = 0.5f;
    }

    public void AddAutoScrollMultiplier(int multiplier)
    {
        if (autoScrollSpeed == 0)
        {
            autoScrollSpeed = -5;
            return;
        }

        autoScrollSpeed *= multiplier;
    }

    public void AddGoldPaperChance(int value)
    {
        goldPaperChance += value;
    }

    public void AddPullPower(int value)
    {
        setScrollSpeed -= value;
    }
}
