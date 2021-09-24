using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionSlides : MonoBehaviour
{
    [SerializeField] private GameObject previous = default;
    [SerializeField] private GameObject next = default;

    [SerializeField] private List<GameObject> slides = new List<GameObject>();

    int currentSlide = 0;

    // Start is called before the first frame update
    void Start()
    {
        previous.SetActive(false);
        next.SetActive(true);

        slides[0].SetActive(true);

        for (int i = 1; i < slides.Count; i++)
            slides[i].SetActive(false);
    }

    public void NextSlide()
    {
        if (currentSlide >= slides.Count - 1) return;

        slides[currentSlide].SetActive(false);

        currentSlide++;

        slides[currentSlide].SetActive(true);

        if (currentSlide >= slides.Count - 1)
            next.SetActive(false);
        else previous.SetActive(true);
    }

    public void PreviousSlide()
    {
        Debug.Log("Hi");

        if (currentSlide <= 0) return;

        slides[currentSlide].SetActive(false);

        currentSlide--;

        slides[currentSlide].SetActive(true);

        if (currentSlide <= 0)
            previous.SetActive(false);
        else next.SetActive(true);
    }
}
