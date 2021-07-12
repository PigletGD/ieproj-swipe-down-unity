using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Duration
{
    INSTANTANEOUS,
    LASTING
};

public abstract class StatusEffect
{
    public string name;
    public abstract void ApplyEffect(GameObject enemy);

    public Duration durationType = Duration.INSTANTANEOUS;
    public float duration;
}
