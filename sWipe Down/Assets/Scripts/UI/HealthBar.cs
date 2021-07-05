using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider = null;

    [SerializeField] Gradient gradient = null;

    [SerializeField] Image fill = null;
    [SerializeField] Image background = null;
    [SerializeField] Image border = null;

    public void OnEnable() => SetImageEnable(false);

    public void SetHealth(int health)
    {
        StopAllCoroutines();

        SetImageEnable(true);

        StartCoroutine(SlideHealth(health));
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    IEnumerator SlideHealth(int health)
    {
        while (slider.value > health)
        {
            float newVal = Mathf.Lerp(slider.value, (float)health, 0.1f);

            if (newVal - health > 0.001f) slider.value = newVal;
            else slider.value = health;

            fill.color = gradient.Evaluate(slider.value / slider.maxValue);

            yield return null;
        }
    }

    private void SetImageEnable(bool value)
    {
        fill.gameObject.SetActive(value);
        background.gameObject.SetActive(value);
        border.gameObject.SetActive(value);
    }
}
