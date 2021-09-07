using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTowerBehaviour : TowerBehaviour
{
    [SerializeField] private SphereCollider collider;
    private int healValue = 1;
    public override void ExecuteAction()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, collider.radius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Building")
            {
                Debug.Log("heal");
                collider.gameObject.GetComponent<HealthComponent>().heal(healValue);
            }
        }
    }

    public override bool ReadyToExecuteAction()
    {
        if (time >= stats.fireRate) return true;

        return false;
    }
}
