﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class EventVector3 : UnityEvent<Transform> { }

public class MouseManager : MonoBehaviour
{
    private bool onMobile = false;

    public LayerMask clickableLayer;

    public Texture2D pointer = null;
    public Texture2D target = null;
    public Texture2D doorway = null;

    public EventVector3 OnClickEnvironment;

    private bool _useDefaultCursor = false;

    // Building Placing Stuff [remove later]
    public GameObject player = null;
    public GameObject currentlySelected = null;
    public GameObject currentlyPlacing = null;

    // Test prefab for building
    public GameObject objectFollow = null;
    [SerializeField] GameObject testBuilding = null;
    [SerializeField] float tileSize = 0;

    // Camera Transformation
    public Transform cameraRig = null;
    public Transform cameraTransform = null;
    public Camera mainCam = null;

    // Zoom
    [SerializeField] int zoomMin = 0;
    [SerializeField] int zoomMax = 0;

    Vector3 newPosition = Vector3.zero;
    Vector3 dragStartPosition = Vector3.zero;
    Vector3 dragCurrentPosition = Vector3.zero;
    int zoomAmount = 0;

    public float cameraMovementTime = 0;

    public bool reachedDestination = false;
    public bool onUI = false;

    private GameManager gameManager = null;

    private bool isMovingMode = true;
    private bool isHolding = false;
    private Vector3 lastCursorPos = Vector3.zero;

    private void Awake()
    {
#if UNITY_ANDROID
        onMobile = true;
#else
        onMobile = false;
#endif

        //GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        newPosition = cameraRig.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!onMobile) MouseControls();
        else MobileControls();
    }

    private void MouseControls()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200, clickableLayer.value))
        {
            Vector3 cursorPos;

            if (objectFollow != null)
            {
                if (IsPointerOverUIObject())
                {
                    objectFollow.SetActive(false);
                    return;
                }
                else objectFollow.SetActive(true);

                cursorPos = new Vector3(Mathf.FloorToInt(hit.point.x / tileSize) * tileSize + (tileSize * 0.5f), 0f, Mathf.FloorToInt(hit.point.z / tileSize) * tileSize + (tileSize * 0.5f));

                // Movement
                objectFollow.transform.position = cursorPos;

                // Input
                if (Input.GetMouseButtonDown(0))
                {
                    // Do world stuff here
                    if (!onUI)
                    {
                        BuildingManager.instance.InstantiateBuilding(cursorPos);
                    }
                }
            }
        }

        // MOVING THROUGH MAP
        if (!IsPointerOverUIObject())
        {
            if (Input.GetMouseButtonDown(2))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry)) dragStartPosition = ray.GetPoint(entry);
            }
            if (Input.GetMouseButton(2))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);
                    newPosition = cameraRig.position + dragStartPosition - dragCurrentPosition;
                    //cameraRig.position = new Vector3(newPosition.x, cameraRig.position.y, newPosition.z);
                }

                cameraRig.position = Vector3.Lerp(cameraRig.position, newPosition, Time.deltaTime * cameraMovementTime);
            }
            else
            {
                cameraRig.position = Vector3.Lerp(cameraRig.position, newPosition, Time.deltaTime * cameraMovementTime * 0.5f);
            }

            // ZOOMING IN
            if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
            {
                if (zoomAmount < zoomMax)
                {
                    mainCam.orthographicSize += 1;
                    zoomAmount++;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // backwards
            {
                if (zoomAmount > zoomMin)
                {
                    mainCam.orthographicSize -= 1;
                    zoomAmount--;
                }
            }
        }
        else
        {
            cameraRig.position = Vector3.Lerp(cameraRig.position, newPosition, Time.deltaTime * cameraMovementTime * 0.5f);
        }

        
    }

    private void MobileControls()
    {
        if (isMovingMode) MoveCam();
        else BuildTower();
    }

    private void MoveCam()
    {
        // MOVING THROUGH MAP
        if (!IsPointerOverUIObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry)) dragStartPosition = ray.GetPoint(entry);
            }
            if (Input.GetMouseButton(0))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);
                    newPosition = cameraRig.position + dragStartPosition - dragCurrentPosition;
                    //cameraRig.position = new Vector3(newPosition.x, cameraRig.position.y, newPosition.z);
                }

                cameraRig.position = Vector3.Lerp(cameraRig.position, newPosition, Time.deltaTime * cameraMovementTime);
            }
            else
            {
                cameraRig.position = Vector3.Lerp(cameraRig.position, newPosition, Time.deltaTime * cameraMovementTime * 0.5f);
            }
        }
        else
        {
            cameraRig.position = Vector3.Lerp(cameraRig.position, newPosition, Time.deltaTime * cameraMovementTime * 0.5f);
        }
    }

    private void BuildTower()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200, clickableLayer.value))
        {
            Vector3 cursorPos;

            if (objectFollow != null)
            {
                if (Input.GetMouseButton(0))
                {
                    if (!isHolding) isHolding = true;

                    if (IsPointerOverUIObject())
                    {
                        objectFollow.SetActive(false);
                        return;
                    }
                    else objectFollow.SetActive(true);

                    cursorPos = new Vector3(Mathf.FloorToInt(hit.point.x / tileSize) * tileSize + (tileSize * 0.5f), 0f, Mathf.FloorToInt(hit.point.z / tileSize) * tileSize + (tileSize * 0.5f));

                    // Movement
                    objectFollow.transform.position = cursorPos;

                    lastCursorPos = cursorPos;
                }
                // Input
                if (Input.GetMouseButtonUp(0))
                {
                    if (isHolding) isHolding = false;

                    int x = (int)(lastCursorPos.x - 0.5f);
                    int y = (int)(lastCursorPos.z - 0.5f);

                    if (CheckLimits(x, y)) BuildingManager.instance.InstantiateBuilding(lastCursorPos);
                    else objectFollow.SetActive(false);

                }

                if (!isHolding && objectFollow != null) objectFollow.SetActive(false);
            }
        }
    }

    private bool CheckLimits(int x, int y)
    {
        if (x >= -2 && x <= 2 && y >= -2 && y <= 2)
            return false;

        return true;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void DeletePlacingInstance()
    {
        Destroy(currentlyPlacing);
        currentlyPlacing = null;
    }

    public void TurnOnMovingMode() => isMovingMode = true;

    public void TurnOnBuildingMode() => isMovingMode = false;
}