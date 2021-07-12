using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Note:  Stacking Effect
public class SlowStatus : StatusEffect
{
    public SlowStatus(float slowPercent)
    {
        this.slowPercentValue = slowPercent;
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
