using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : TowerBehaviour
{
    public bool isAroma = false;

    public override void ExecuteAction()
    {
        Debug.Log("Not supposed to have funtionality");
    }

    public override bool ReadyToExecuteAction()
    {
        return false;
    }
}
