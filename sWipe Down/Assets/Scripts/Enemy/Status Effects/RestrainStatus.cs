using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrainStatus : StatusEffect
{
    public RestrainStatus(float dur)
    {
        duration = dur;
        durationType = Duration.LASTING;
    }

    public override void ApplyEffect(GameObject enemy)
    {
        enemy.GetComponent<EnemyMove>().speed = 0;
    }
}
