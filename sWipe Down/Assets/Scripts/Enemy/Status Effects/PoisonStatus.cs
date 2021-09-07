using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStatus : StatusEffect
{
    public PoisonStatus(float dur)
    {
        duration = dur;
        durationType = Duration.LASTING;
        this.statusType = StatusType.Slow;
    }

    public override void ApplyEffect(GameObject enemy)
    {
        HealthComponent healthComponent = enemy.GetComponentInChildren<HealthComponent>();
        
        if (healthComponent != null)
        {
            //healthComponent.TakeDamage(1);
            Debug.Log("Take Damage");
        }
    }
}
