using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPGenerationBehaviour : TowerBehaviour
{
    [SerializeField] private int basegain = 10;

    public override void ExecuteAction()
    {
        manager.IncrementScore(basegain);
    }

    public override bool ReadyToExecuteAction()
    {
        if (time >= fireRate) return true;
        return false;
    }
}
