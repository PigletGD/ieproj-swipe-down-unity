using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingToggle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Vector2 toggleOnPosition = Vector3.zero;
    [SerializeField] Vector2 toggleOffPosition = Vector3.zero;

    [SerializeField] RectTransform parent = null;
    [SerializeField] RectTransform rectTransform = null;

    private bool toggleOn = true;

    // Start is called before the first frame update
    void Start()
    {
        toggleOnPosition = parent.anchoredPosition;
        toggleOffPosition = new Vector2(parent.anchoredPosition.x, parent.anchoredPosition.y - rectTransform.rect.height);
        //Debug.Log(parent.anchoredPosition.x - rectTransform.rect.width);
    }

    // Update is called once per frame
    void Update()
    {
        float lerp;

        //Debug.Log(parent.position.x);

        if (toggleOn) lerp = Mathf.Lerp(parent.anchoredPosition.y, toggleOnPosition.y, 0.1f);
        else lerp = Mathf.Lerp(parent.anchoredPosition.y, toggleOffPosition.y, 0.1f);

        parent.anchoredPosition = new Vector2(parent.anchoredPosition.x, lerp);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        toggleOn = !toggleOn;
    }
}