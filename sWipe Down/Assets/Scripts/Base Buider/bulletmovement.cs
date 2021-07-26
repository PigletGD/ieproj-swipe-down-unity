using System.Collections;
using UnityEngine;

public class bulletmovement : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Die");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<HealthComponent>().TakeDamage(2);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 0.1f;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
