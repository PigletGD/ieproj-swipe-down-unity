using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<ObjectPool> enemyPools = default;
    private int spawnCount = 0;
    private float spawnRadius = 0f;
    private int initialMinTimer = 0;
    private int initialMaxTimer = 0;
    private float radius = 0;
    private int minTimer = 0;
    private int maxTimer = 0;
    private int remainingEnemies = 0;
    private int timerInteger = 0;
    private float timerForNextSpawn = 0f;
    private float currentTimerForNextSpawn = 0f;
    private bool waveOngoing = false;

    private bool startEnemyWaveSpawning = false;

    [SerializeField] Text waveMessage = null;
    [SerializeField] Slider waveSlider = null;
    [SerializeField] Animator sliderAnimator = null;

    [ContextMenuItem("Organize Waves", "OrganizeWaves")]
    [SerializeField] List<WaveSO> waves = default;

    List<int> enemyType = new List<int>();

    [SerializeField] private Transform baseTransform;
    [SerializeField] private float spawnDistanceFromBase;
    [SerializeField] private float spawnWidth;
    [SerializeField] private float spawnLength;

    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }

    private void Update()
    {
        if (!waveOngoing && startEnemyWaveSpawning)
        {
            currentTimerForNextSpawn += Time.deltaTime;

            waveSlider.value = currentTimerForNextSpawn;

            if (currentTimerForNextSpawn > timerForNextSpawn) SpawnEnemyWave();
            //else if (timerForNextSpawn <= (float)timerInteger) UpdateTimerText();
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
        sliderAnimator.SetTrigger("WaveStart");

        int direction = Random.Range(0, 4);

        GameObject GO;
        for (int i = 0; i < spawnCount; i++)
        {
            GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
            //GO.transform.position = RandomCircle();
            GO.transform.position = RandomAtDirection(direction);
        }

        remainingEnemies = spawnCount;
        waveOngoing = true;

        waveMessage.text = "There are " + remainingEnemies + " Enemies Remaining";
    }

    public void KilledEnemy()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            waveOngoing = false;

            currentTimerForNextSpawn = 0;
            timerForNextSpawn = Random.Range(minTimer, maxTimer);

            waveSlider.minValue = 0;
            waveSlider.maxValue = timerForNextSpawn;

            sliderAnimator.SetTrigger("WaveEnd");

            //UpdateTimerText();
        }
        else waveMessage.text = "There are " + remainingEnemies + " Enemies Remaining";
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

    Vector3 RandomAtDirection(int direction)
    {
        Vector3 pos = new Vector3(0.5f, 0.0f, 0.5f);
        pos.y = 0;

        Vector3 delta = Vector3.zero;

        float randomLength = Random.Range(spawnLength * -0.5f, spawnLength * 0.5f);
        float randomWidth = Random.Range(spawnWidth * -0.5f, spawnWidth * 0.5f);

        switch (direction)
        {
            case 0:
                delta.x = -spawnDistanceFromBase + randomLength;
                delta.z = randomWidth;
                break;
            case 1:
                delta.x = spawnDistanceFromBase + randomLength;
                delta.z = randomWidth;
                break;
            case 2:
                delta.z = -spawnDistanceFromBase + randomLength;
                delta.x = randomWidth;
                break;
            case 3:
                delta.z = -spawnDistanceFromBase + randomLength;
                delta.x = randomWidth;
                break;
        }

        return pos + delta;
    }

    public void AdvanceToNextWave()
    {
        if (waves.Count <= 0) return;

        waveSlider.gameObject.SetActive(true);

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

            waveSlider.minValue = 0;
            waveSlider.maxValue = timerForNextSpawn;

            startEnemyWaveSpawning = true;
        }

        //UpdateTimerText();
    }

    [ContextMenu("Organize Waves")]
    public void OrganizeWaves()
    {
        waves = waves.OrderBy(e => e.waveNumber).ToList();
        
        Debug.Log("Finished Organizing Waves");
    }
}