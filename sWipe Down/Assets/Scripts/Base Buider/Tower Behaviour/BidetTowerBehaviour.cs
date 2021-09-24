 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidetTowerBehaviour : TowerBehaviour
{
    [SerializeField] bool isPoision = false;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzle;

    public override void ExecuteAction()
    {
        Vector3 direction = new Vector3(target.position.x, 0f, target.position.z);
        transform.LookAt(direction);

        GameObject temp = Instantiate(bullet, muzzle.transform.position, Quaternion.identity);
        temp.transform.LookAt(direction);

        if (isPoision)
        {
            targetList.Remove(target);
            targetList.Add(target);
            target = targetList[0];
        }
    }

    public override bool ReadyToExecuteAction()
    {
        if (time >= stats.fireRate && target != null) return true;

        return false;
    }
}