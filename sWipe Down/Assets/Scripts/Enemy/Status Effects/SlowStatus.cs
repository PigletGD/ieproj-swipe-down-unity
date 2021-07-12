using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Note:  Stacking Effect
public class SlowStatus : StatusEffect
{
    public SlowStatus(float slowPercentValue)
    {
        this.slowPercentValue = slowPercentValue;
        duration = 0;
        durationType = Duration.INSTANTANEOUS;
    }

    private float slowPercentValue;

    public override void ApplyEffect(GameObject enemy)
    {
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.speed *= slowPercentValue;
    }
}
