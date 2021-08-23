using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;

    [SerializeField] private AudioSource Audio;
    [SerializeField] private AudioSource Audio1;
    [SerializeField] private AudioSource Audio2;

    public static MusicManager GetInstance()
    {
        if (Instance != null)
        {
            Debug.Log("called");
            return Instance;
        }
        else
        {
            return null;
        }

    }

    private void Start()
    {
        Instance = this;
        PlayMenu();
    }

    public void PlayMenu()
    {
        StopAll();
        Audio.Play();
    }

    public void PlayStage()
    {
        StopAll();
        Audio1.Play();
    }

    public void PlayBoss()
    {
        StopAll();
        Audio2.Play();
    }

    private void StopAll()
    {
        Audio.Stop();
        Audio1.Stop();
        Audio2.Stop();
    }
}
