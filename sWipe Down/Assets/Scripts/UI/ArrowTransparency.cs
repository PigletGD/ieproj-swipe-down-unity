using System;
using UnityEngine;
using UnityEngine.UI;

public class ArrowTransparency : MonoBehaviour
{
    [SerializeField] Image image = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float alpha = Mathf.Lerp(image.color.a, 0f, 0.025f);
            image.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            float alpha = Mathf.Lerp(image.color.a, 0.8f, 0.025f);
            image.color = new Color(1, 1, 1, alpha);
        }
    }
}
