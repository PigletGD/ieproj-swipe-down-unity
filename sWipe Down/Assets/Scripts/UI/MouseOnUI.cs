using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private MouseManager mouseManager = null;

    private void Awake()
    {
        //mouseManager = FindObjectOfType<MouseManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseManager.onUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseManager.onUI = false;
    }
}
