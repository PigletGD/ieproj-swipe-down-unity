using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingToggle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Vector2 toggleOnPosition = Vector3.zero;
    [SerializeField] Vector2 toggleOffPosition = Vector3.zero;

    [SerializeField] float arbitraryNumber;

    [SerializeField] RectTransform parent = null;
    [SerializeField] RectTransform rectTransform = null;

    [SerializeField] CanvasScaler canvas = null;
    [SerializeField] LayoutElement LE = null;

    private bool toggleOn = true;
    // Start is called before the first frame update
    void Start()
    {

        toggleOnPosition = new Vector2(0, 0);
        toggleOffPosition = new Vector2(0, -LE.minHeight);
    }

    // Update is called once per frame
    void Update()
    {
        float lerp;

        //Debug.Log(parent.position.x);

        if (toggleOn) lerp = Mathf.Lerp(parent.anchoredPosition.y, toggleOnPosition.y, 0.1f);
        else lerp = Mathf.Lerp(parent.anchoredPosition.y, toggleOffPosition.y, 0.1f);

        parent.anchoredPosition = new Vector2(toggleOnPosition.x, lerp);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        toggleOn = !toggleOn;
    }
}