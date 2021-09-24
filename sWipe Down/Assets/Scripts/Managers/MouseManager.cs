using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer;

    // Test prefab for building
    public GameObject objectFollow = null;
    [SerializeField] float tileSize = 0;
    [SerializeField] float tileOffsetX = 0;
    [SerializeField] float tileOffsetZ = 0;

    // Camera Transformation
    public Transform cameraRig = null;
    public Transform cameraTransform = null;
    public Camera mainCam = null;

    // Zoom
    [SerializeField] int zoomMin = 0;
    [SerializeField] int zoomMax = 0;

    public Vector3 newPosition = Vector3.zero;
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

    [SerializeField] Image icon;
    [SerializeField] RectTransform iconImage;
    [SerializeField] Sprite[] iconSprites;

    private void Awake()
    {
/*#if UNITY_ANDROID
        onMobile = true;
#else
        onMobile = false;
#endif*/

        //GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        newPosition = cameraRig.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        MouseControls();

        UpdateControls();
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
                    iconImage.gameObject.SetActive(true);

                    int index = BuildingManager.instance.currentBuildingType;
                    if (index < iconSprites.Length) icon.sprite = iconSprites[index];
                    else iconImage.gameObject.SetActive(false);

                    iconImage.position = Input.mousePosition;
                    return;
                }
                else
                {
                    objectFollow.SetActive(true);
                    iconImage.gameObject.SetActive(false);
                }

                cursorPos = new Vector3((Mathf.FloorToInt((hit.point.x - (tileOffsetX)) / tileSize) * tileSize + (tileSize * 0.5f)) + tileOffsetX, 0f, (Mathf.FloorToInt((hit.point.z - tileOffsetZ) / tileSize) * tileSize + (tileSize * 0.5f)) + tileOffsetZ);
                Vector3 checkPos = new Vector3((cursorPos.x - tileOffsetX - (tileSize * 0.5f)) / tileSize, 0, (cursorPos.z - tileOffsetZ - (tileSize * 0.5f)) / tileSize);

                if (BuildingManager.instance.CheckIfTileOccupied(checkPos))
                {
                    objectFollow.SetActive(false);
                }
                else
                {
                    objectFollow.SetActive(true);

                    // Movement
                    objectFollow.transform.position = cursorPos;

                    // Input
                    if (Input.GetMouseButton(0))
                    {
                        // Do world stuff here
                        if (!IsPointerOverUIObject())
                        {
                            BuildingManager.instance.InstantiateBuilding(checkPos, cursorPos);
                        }
                    }
                }
            }
            else
            {
                iconImage.gameObject.SetActive(false);
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

            if (Input.GetMouseButtonDown(1))
            {
                BuildingManager.instance.ChangeCurrentlySelectedBuilding(-1);
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

                    //if (CheckLimits(x, y)) BuildingManager.instance.InstantiateBuilding(lastCursorPos);
                    //else objectFollow.SetActive(false);

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

    public void TurnOnMovingMode() => isMovingMode = true;

    public void TurnOnBuildingMode() => isMovingMode = false;

    #region InputSystem

    private float forward = 0.0f;
    private float rightward = 0.0f;
    private float speed = 5.0f;
    private float shiftMultiplier = 1.0f;
    private void UpdateControls()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            forward += speed;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            forward -= speed;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rightward -= speed;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rightward += speed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftMultiplier = 3.0f;
        }


        if (Input.GetKeyUp(KeyCode.W))
        {
            forward -= speed;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            forward += speed;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rightward += speed;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rightward -= speed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shiftMultiplier = 1.0f;
        }


        newPosition += (cameraRig.transform.forward + cameraRig.transform.right) * forward * shiftMultiplier * Time.deltaTime;
        newPosition += (-cameraRig.transform.forward + cameraRig.transform.right) * rightward * shiftMultiplier * Time.deltaTime;

        //newPosition.Translate((cameraRig.transform.forward + cameraRig.transform.right) * forward * shiftMultiplier * Time.deltaTime);
        //newPosition.Translate((-cameraRig.transform.forward + cameraRig.transform.right) * rightward * shiftMultiplier * Time.deltaTime);


    }
    #endregion
}