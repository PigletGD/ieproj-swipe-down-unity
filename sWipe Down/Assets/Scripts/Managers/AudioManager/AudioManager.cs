using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    // Start is called before the first frame update

    [SerializeField]private List<Sound> soundList;
    [SerializeField]private Dictionary<string, Sound> soundMap;

    void Awake()
    {
        AudioManager.instance = this;

        soundMap = new Dictionary<string, Sound>();

        foreach (Sound sound in soundList)
        {
            sound.source = this.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

            soundMap.Add(sound.name, sound);
        }
    }

    public void Play(string name)
    {
        if (!soundMap.ContainsKey(name))
        {
            Debug.LogWarning("Sound " + name + " Not found");
            return;
        }

        Sound sound = soundMap[name];

        if (sound.source.isPlaying)
        {
            sound.source.Stop();
        }
        sound.source.Play();
    }

    public void Stop(string name)
    {
        if (!soundMap.ContainsKey(name))
        {
            Debug.LogWarning("Sound " + name + " Not found");
            return;
        }

        Sound sound = soundMap[name];
        sound.source.Stop();
       
    }
}
