using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionStatus : StatusEffect
{
    public PollutionStatus(float slowPercent, float dur)
    {
        this.slowPercentValue = slowPercent;
        duration = dur;
        durationType = Duration.LASTING;
        this.statusType = StatusType.Slow;
    }

    private float slowPercentValue;

    public override void ApplyEffect(GameObject tower)
    {
        TowerBehaviour behaviour = tower.GetComponent<TowerBehaviour>();
        behaviour.fireRate = behaviour.maxFireRate * slowPercentValue;
    }
}
