using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStatus : StatusEffect
{
    int damage = 0;
    float currentTick = 0.0f;

    public PoisonStatus(int damageValue, float dur)
    {
        damage = damageValue;
        duration = dur;
        durationType = Duration.LASTING;
        statusType = StatusType.Poison;
    }

    public override void ApplyEffect(GameObject enemy)
    {
        currentTick += Time.deltaTime;

        if (currentTick > 1.0f)
        {
            HealthComponent healthComponent = enemy.GetComponentInChildren<HealthComponent>();

            if (healthComponent != null)
            {
                healthComponent.TakeDamage(damage);
            }

            currentTick -= 1.0f;
        }
    }
}
