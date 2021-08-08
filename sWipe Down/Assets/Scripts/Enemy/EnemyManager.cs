using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<ObjectPool> enemyPools = default;
    [SerializeField] int spawnCount = 0;
    [SerializeField] float spawnRadius = 0f;
    [SerializeField] int initialMinTimer = 0;
    [SerializeField] int initialMaxTimer = 0;
    private float radius = 0;
    private int minTimer = 0;
    private int maxTimer = 0;
    private int remainingEnemies = 0;
    private int timerInteger = 0;
    private float timerForNextSpawn = 0f;
    private bool waveOngoing = false;

    private bool startEnemyWaveSpawning = false;

    [SerializeField] Text waveMessage = null;

    [SerializeField] List<WaveSO> waves = default;

    List<int> enemyType = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }

    private void Update()
    {
        if (!waveOngoing && startEnemyWaveSpawning)
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
        GameObject GO;
        for (int i = 0; i < spawnCount; i++)
        {
            GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
            GO.transform.position = RandomCircle();
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

            timerForNextSpawn = Random.Range(minTimer, maxTimer);

            UpdateTimerText();
        }
        else waveMessage.text = "There are " + remainingEnemies + " enemies remaining";
    }

    void ResetValues()
    {
        startEnemyWaveSpawning = false;

        waveMessage.text = "";

        //spawnCount = initialSpawnCount;
        minTimer = initialMinTimer;
        maxTimer = initialMaxTimer;

        timerForNextSpawn = Random.Range(minTimer, maxTimer);
        timerInteger = (int)timerForNextSpawn - 1;

        radius = spawnRadius;

        //UpdateTimerText();
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

    public void AdvanceToNextWave()
    {
        if (waves.Count <= 0) return;

        spawnCount = waves[0].spawnNumber;

        minTimer = waves[0].initialMinTimer;
        maxTimer = waves[0].initialMaxTimer;

        enemyType.Clear();

        for (int i = 0; i < waves[0].enemyTypes.Length; i++)
            enemyType.Add(waves[0].enemyTypes[i]);

        waves.RemoveAt(0);

        if (!startEnemyWaveSpawning)
        {
            timerForNextSpawn = Random.Range(minTimer, maxTimer);
            timerInteger = (int)timerForNextSpawn - 1;

            startEnemyWaveSpawning = true;
        }

        UpdateTimerText();
    }
}