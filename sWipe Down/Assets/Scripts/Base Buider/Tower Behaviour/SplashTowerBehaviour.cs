using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTowerBehaviour : TowerBehaviour
{
    [SerializeField] private SphereCollider splashCollider = default;

    public override void ExecuteAction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, splashCollider.radius);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                HealthComponent healthComponent = collider.gameObject.GetComponent<HealthComponent>();
                if (healthComponent != null && healthComponent.gameObject != this.gameObject)
                {
                    healthComponent.TakeDamage((int)stats.actionValue);
                }
            }
        }
        Debug.Log("Explode");
    }

    public override bool ReadyToExecuteAction()
    {
        if (time >= stats.fireRate && target != null) return true;

        return false;
    }
}