using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Duration
{
    INSTANTANEOUS,
    LASTING
};


public enum StatusType
{ 
    Slow = 0,
    Restrain = 1,
    Poison = 2
}

public abstract class StatusEffect
{
    public string name;
    public abstract void ApplyEffect(GameObject target);
    public void SetParticleSystem(GameObject particleSystem)
    {
        if (particleSystem != null)
        {
        }
    }

    public Duration durationType = Duration.INSTANTANEOUS;
    public float duration;
    public StatusType statusType;

    private GameObject particleSystemRef;
}
