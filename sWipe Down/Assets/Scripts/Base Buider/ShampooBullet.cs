using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShampooBullet : MonoBehaviour
{
    public float slowPercentValue = 0.75f;
    private void Start()
    {
        StartCoroutine("Die");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMove>().AddStatusEffect(new SlowStatus(slowPercentValue, 0.25f));
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
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
