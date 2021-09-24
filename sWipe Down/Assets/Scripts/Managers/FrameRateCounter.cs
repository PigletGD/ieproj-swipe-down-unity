using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRateCounter : MonoBehaviour
{
    //public enum DisplayMode { FPS, MS }

    [Header("UI Setup")]
    [SerializeField] private Text display = default;

    [Header("FPS Display Settings")]
    //[SerializeField] private DisplayMode displayMode = DisplayMode.FPS;
    [SerializeField] private float sampleDuration = 1.0f;

    int frames = 0;
    float duration = 0.0f;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        float frameDuration = UpdateFrames();

        if (duration >= sampleDuration)
        {
            DisplayFPS();
            ResetValues();
        }
    }

    private float UpdateFrames()
    {
        float frameDuration = Time.unscaledDeltaTime;
        duration += frameDuration;
        frames += 1;

        return frameDuration;
    }

    private void DisplayFPS()
    {
        display.text = "FPS: " + ((int)(frames / duration)).ToString();
    }

    private void ResetValues()
    {
        frames = 0;
        duration = 0.0f;
    }
}
