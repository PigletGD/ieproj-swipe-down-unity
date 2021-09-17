using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusParticleSystem : MonoBehaviour
{
    public float elapsedTime = 0.0f;
    public float statusDuration = 0.0f;
    public StatusType statusType;

    private void Update()
    {
        if (elapsedTime > statusDuration)
            this.gameObject.SetActive(false);
        elapsedTime += Time.fixedDeltaTime;
    }

    public void StatusUpdate(StatusEffect effect)
    {
        if (statusDuration != effect.duration)
            statusDuration = effect.duration;
        elapsedTime = 0.0f;
        
        this.gameObject.SetActive(true);

        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
        particleSystem.Play();
    }
}
