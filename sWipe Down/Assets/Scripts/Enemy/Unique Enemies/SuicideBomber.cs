using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomber : MonoBehaviour, IAttackHandler, IDeathHandler
{
    [SerializeField] SphereCollider explosionRadius; // This is used to get range and also visualize the range
    [SerializeField] GameObject explosionParticleSystem;
    public int damage;

    private void Start()
    {
        GetComponent<HealthComponent>().AddDeathHandler(this);
        GetComponent<EnemyMove>().AddAttackHandler(this);
    }

    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius.radius);

        foreach (Collider collider in colliders)
        {
            HealthComponent healthComponent = collider.gameObject.GetComponent<HealthComponent>();
            if (healthComponent != null && healthComponent.gameObject != this.gameObject)
            {
                healthComponent.TakeDamage(damage);
            }
        }
        Debug.Log("Explode");
    }

    public void OnAttack()
    {
        gameObject.GetComponent<HealthComponent>().TakeDamage(10000);
    }

    public void OnDeath()
    {
        Explode();
        GameObject temp = Instantiate(explosionParticleSystem);
        temp.transform.position = this.transform.position;
        temp.transform.Translate(new Vector3(0.0f, 0.0f, 0.5f));
        Destroy(temp, 2f);
    }
}
