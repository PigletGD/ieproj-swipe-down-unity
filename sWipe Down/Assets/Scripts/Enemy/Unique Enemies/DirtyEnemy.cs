using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyEnemy : EnemyMove
{
    [SerializeField] float fireRateReduction;
    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "Building" || collision.gameObject.tag == "Base") && timeElapsed > attackRate)
        {
            timeElapsed = 0;
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(1);
            collision.gameObject.GetComponent<TowerBehaviour>().AddStatusEffect(new PollutionStatus(fireRateReduction, 0.5f));
            OnAttack();
        }
    }
}
