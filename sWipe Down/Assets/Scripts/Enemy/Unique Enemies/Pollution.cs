using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : MonoBehaviour
{
    [SerializeField] SphereCollider range;
    [SerializeField] float fireRateReduction;
    [SerializeField] float fireRate;
    float elapsedTime = 0;

    private void Pollute(TowerBehaviour tower)
    {
        tower.AddStatusEffect(new PollutionStatus(fireRateReduction, 0.5f));
    }

    private void Update()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime >= fireRate)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range.radius);

            foreach (Collider collider in colliders)
            {
                TowerBehaviour tower = collider.gameObject.GetComponent<TowerBehaviour>();

                if (collider.gameObject.tag == "Building" && tower != null)
                {
                    Pollute(tower);
                }
            }

            elapsedTime -= fireRate;
        }
    }




}
