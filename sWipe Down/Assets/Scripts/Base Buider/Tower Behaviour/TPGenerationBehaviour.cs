using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPGenerationBehaviour : TowerBehaviour
{
    public override void ExecuteAction()
    {
        manager.IncrementScore((int)stats.actionValue);
    }

    public override bool ReadyToExecuteAction()
    {
        if (time >= stats.fireRate) return true;
        return false;
    }
}
