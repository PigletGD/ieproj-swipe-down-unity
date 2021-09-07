using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour, IDeathHandler
{
    [SerializeField] int offspringCount;

    private void Start()
    {
        GetComponent<HealthComponent>().AddDeathHandler(this);
    }

    public void OnDeath()
    {
        GameObject manager = GameObject.Find("Enemy Manager");
        if (manager != null)
        {
            for (int i = 0; i < offspringCount; i++)
            {
                manager.GetComponent<EnemyManager>().SpawnEnemy(0, this.transform.position);
            }
        }
    }
}
