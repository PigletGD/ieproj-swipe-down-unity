using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainTileBehaviour : TowerBehaviour
{
    private bool activated = false;
    // Start is called before the first frame update
    public override void ExecuteAction()
    {
        if (!activated)
        {
            target.GetComponent<EnemyMove>().AddStatusEffect(new RestrainStatus(3));
            activated = true;
        }

        StartCoroutine("DestroyDrain");
    }

    public override bool ReadyToExecuteAction()
    {
        if (target != null) return true;
        return false;
    }

    IEnumerator DestroyDrain()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }
}
