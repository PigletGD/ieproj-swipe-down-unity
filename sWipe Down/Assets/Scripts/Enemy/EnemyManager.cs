using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyTypes = null;
    [SerializeField] int initialSpawnCount = 0;
    [SerializeField] float spawnRadius = 0f;
    [SerializeField] int initialMinTimer = 0;
    [SerializeField] int initialMaxTimer = 0;
    private float radius = 0;
    private int minTimer = 0;
    private int maxTimer = 0;
    private int spawnCount = 0;
    private int remainingEnemies = 0;
    private int timerInteger = 0;
    private float timerForNextSpawn = 0f;
    private bool waveOngoing = false;

    [SerializeField] Text waveMessage = null;

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }

    private void Update()
    {
        if (!waveOngoing)
        {
            timerForNextSpawn -= Time.deltaTime;

            if (timerForNextSpawn <= 0) SpawnEnemyWave();
            else if (timerForNextSpawn <= (float)timerInteger) UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int minute = (int)(timerForNextSpawn / 60);
        int seconds = (int)(timerForNextSpawn % 60) + 1;

        if (seconds >= 10) waveMessage.text = "Time until next wave: " + minute + ":" + seconds;
        else waveMessage.text = "Time until next wave: " + minute + ":0" + seconds;
    }

    void SpawnEnemyWave()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            Instantiate(enemyTypes[0], RandomCircle(), Quaternion.identity);
        }

        remainingEnemies = spawnCount;
        waveOngoing = true;

        waveMessage.text = "There are " + remainingEnemies + " enemies remaining";
    }

    public void KilledEnemy()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            waveOngoing = false;

            if (minTimer > 10) minTimer -= 3;
            if (maxTimer > 10) maxTimer -= 3;

            timerForNextSpawn = Random.Range(minTimer, maxTimer);

            if (spawnCount < 30) spawnCount += 2;

            if (radius < 25) radius++;

            UpdateTimerText();
        }
        else waveMessage.text = "There are " + remainingEnemies + " enemies remaining";
    }

    void ResetValues()
    {
        spawnCount = initialSpawnCount;
        minTimer = initialMinTimer;
        maxTimer = initialMaxTimer;

        timerForNextSpawn = Random.Range(minTimer, maxTimer);
        timerInteger = (int)timerForNextSpawn - 1;

        radius = spawnRadius;

        UpdateTimerText();
    }

    Vector3 RandomCircle()
    {
        float angle = Random.value * 360;
        Vector3 pos = Vector3.zero;

        pos.x = spawnRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = 0f;
        pos.z = spawnRadius * Mathf.Cos(angle * Mathf.Deg2Rad);

        return pos;
    }
}