﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidetTowerBehaviour : TowerBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject muzzle;

    public override void ExecuteAction()
    {
        Vector3 direction = new Vector3(target.position.x, 0f, target.position.z);
        transform.LookAt(direction);

        GameObject temp = Instantiate(bullet, muzzle.transform.position, Quaternion.identity);
        temp.transform.LookAt(direction);
    }

    public override bool ReadyToExecuteAction()
    {
        if (time >= fireRate && target != null) return true;

        return false;
    }
}