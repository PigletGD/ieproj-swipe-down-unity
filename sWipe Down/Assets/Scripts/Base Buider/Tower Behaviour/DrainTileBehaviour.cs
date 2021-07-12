using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainTileBehaviour : TowerBehaviour
{
    private bool activated = false;
    // Start is called before the first frame update
    public override void ExecuteAction()
    {
        if (activated == false)
        {
            target.GetComponent<EnemyMove>().AddStatusEffect(new RestrainStatus(3));
            Destroy(this.gameObject);
        }
        
    }

    public override bool ReadyToExecuteAction()
    {
        if (target != null) return true;
        return false;
    }
}
