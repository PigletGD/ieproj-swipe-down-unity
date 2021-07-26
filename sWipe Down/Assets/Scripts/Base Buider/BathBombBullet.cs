using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathBombBullet : MonoBehaviour
{
    public float power = 10.0f;
    public float radius = 2.0f;
    public float upforce = 1.0f;

    private void Start()
    {
        StartCoroutine("Die");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Detonate();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 0.1f;
    }

    void Detonate()
    {
        Vector3 explosionPosition = gameObject.transform.position;
        Collider[] colliderList = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider collider in colliderList)
        {
            if (collider.gameObject.GetComponent<EnemyMove>() != null)
            {
                collider.gameObject.GetComponent<HealthComponent>().TakeDamage(5);
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if(rb != null)
                    rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
