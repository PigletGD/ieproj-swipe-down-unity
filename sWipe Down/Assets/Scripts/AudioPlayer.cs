using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public void PlayAudio(string clip)
    {
        AudioManager.instance.Play(clip);
    }
}
