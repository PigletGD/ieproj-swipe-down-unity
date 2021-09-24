using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private bool promptTutorial = false;

    [SerializeField] private GameObject tutorialSlides = default;

    private void Start()
    {
        if (!promptTutorial)
            gameObject.SetActive(false);
    }

    public void ProceedToTutorial()
    {
        tutorialSlides.SetActive(true);
    }

    public void SkipTutorial()
    {
        gameObject.SetActive(false);
    }
}