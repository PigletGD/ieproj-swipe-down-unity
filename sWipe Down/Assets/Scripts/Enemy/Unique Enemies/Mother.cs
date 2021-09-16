using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    EnemyManager EM = default;

    [SerializeField] float spawnDuration = 10.0f;
    private float currentTicks = 0.0f;

    [SerializeField] int spawnCount = 6;

    [SerializeField] float spawnRadius = 5;

    void Start()
    {
        if (EM == null)
        {
            GameObject manager = GameObject.Find("Enemy Manager");

            if (manager != null) EM = manager.GetComponent<EnemyManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTicks += Time.deltaTime;

        if (currentTicks > spawnDuration)
        {
            currentTicks -= spawnDuration;

            if (EM == null)
            {
                GameObject manager = GameObject.Find("Enemy Manager");

                if (manager != null) EM = manager.GetComponent<EnemyManager>();
            }

            for (int i = 0; i < spawnCount; i++)
                EM.SpawnEnemy(Random.Range(0, 7), transform.position, spawnRadius);
        }
    }
}
