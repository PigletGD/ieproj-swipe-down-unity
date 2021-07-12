using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Note:  Stacking Effect
public class SlowStatus : StatusEffect
{
    public SlowStatus(float slowPercent, float dur)
    {
        this.slowPercentValue = slowPercent;
        duration = dur;
        durationType = Duration.LASTING;
    }

    private float slowPercentValue;

    public override void ApplyEffect(GameObject enemy)
    {
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.speed *= slowPercentValue;
    }
}
