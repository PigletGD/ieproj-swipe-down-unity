using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Transform> { }

public class MouseManager : MonoBehaviour
{
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

    private void Awake()
    {
        //GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        newPosition = cameraRig.position;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 200, clickableLayer.value))
        {
            Vector3 cursorPos;

            if (objectFollow != null)
            {
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
        else
        {
            //Debug.Log("Did Not Hit");
            //if (Input.GetMouseButtonDown(0)) GameEvents.current.MouseClickNoTarget();
        }

        // MOVING THROUGH MAP
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
            if(zoomAmount < zoomMax)
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

        //cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * cameraMovementTime);

        /*if (currentlyPlacing != null)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            Transform placing = currentlyPlacing.transform;

            if (plane.Raycast(ray, out entry))
            {
                placing.position = ray.GetPoint(entry);
                placing.position = new Vector3(Mathf.Round(placing.position.x) - 0.5f, placing.position.y, Mathf.Round(placing.position.z) - 0.5f);
            }


        }*/
    }

    /*private Transform GetClosetWaypoint(RaycastHit hit)
    {
        Debug.Log(hit.collider.gameObject.name);

        Transform[] waypoints = hit.collider.gameObject.GetComponent<WaypointHandler>().waypoints;

        Transform targetTransform = player.transform;
        float shortestDistance = Mathf.Infinity;
        float currentDistance = Mathf.Infinity;

        foreach (Transform waypoint in waypoints)
        {
            if (!waypoint.gameObject.activeSelf) continue;

            currentDistance = Vector3.SqrMagnitude(waypoint.position - player.transform.position);

            if (currentDistance < shortestDistance)
            {
                shortestDistance = currentDistance;
                targetTransform = waypoint;
            }
        }

        return targetTransform;
    }

    void ReachedDestination()
    {
        reachedDestination = true;
        currentlySelected.GetComponent<ClickableObject>().TurnOn();
    }

    void ResetVariables()
    {
        reachedDestination = false;
        currentlySelected.GetComponent<ClickableObject>().TurnOff();
    }
    */

    void DeletePlacingInstance()
    {
        Destroy(currentlyPlacing);
        currentlyPlacing = null;
    }
}