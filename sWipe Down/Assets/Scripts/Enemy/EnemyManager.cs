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

    [SerializeField] private GameObject topArrow = default;
    [SerializeField] private GameObject bottomArrow = default;
    [SerializeField] private GameObject rightArrow = default;
    [SerializeField] private GameObject leftArrow = default;

    [SerializeField] private Material arrow = default;

    [SerializeField] private VoidEvent buildingUnlock = default;

    [SerializeField] private GameObject winningPanel = default;

    private bool mainGameWon = false;

    private int spawnDirection = 2;

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

        switch (spawnDirection)
        {
            case 1:
                SpawnOneDirection();
                break;
            case 2:
                SpawnTwoDirection();
                break;
            case 3:
                SpawnThreeDirection();
                break;
            case 4:
                SpawnFourDirection();
                break;
            default:
                SpawnTwoDirection();
                break;
        }

        remainingEnemies = spawnCount;
        waveOngoing = true;

        waveMessage.text = remainingEnemies + " Enemies Remaining";

        StartCoroutine("WarningAnimation");
    }

    private void SpawnOneDirection()
    {
        int direction = Random.Range(0, 4);

        GameObject GO;
        if (direction == 0)
        {
            leftArrow.SetActive(true);

            for (int i = 0; i < spawnCount; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(0);
            }
        }
        else if (direction == 1)
        {
            rightArrow.SetActive(true);

            for (int i = 0; i < spawnCount; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(1);
            }
        }
        else if (direction == 2)
        {
            bottomArrow.SetActive(true);

            for (int i = 0; i < spawnCount; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(2);
            }
        }
        else
        {
            topArrow.SetActive(true);

            for (int i = 0; i < spawnCount; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(3);
            }
        }
    }

    private void SpawnTwoDirection()
    {
        int direction = Random.Range(0, 4);

        GameObject GO;
        if (direction <= 1)
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);

            for (int i = 0; i < spawnCount / 2; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(0);
            }

            for (int i = 0; i < (spawnCount / 2) + (spawnCount % 2); i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(1);
            }
        }
        else
        {
            topArrow.SetActive(true);
            bottomArrow.SetActive(true);

            for (int i = 0; i < spawnCount / 2; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(2);
            }

            for (int i = 0; i < (spawnCount / 2) + (spawnCount % 2); i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(3);
            }
        }
    }

    private void SpawnThreeDirection()
    {
        int direction = Random.Range(0, 4);

        int remainder = spawnCount % 3;
        int addend = 0;

        GameObject GO;
        if (direction <= 1)
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);

            for (int i = 0; i < spawnCount / 3; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(0);
            }

            if (remainder > 0)
            {
                addend = 1;
                remainder -= 1;
            }
            else addend = 0;

            for (int i = 0; i < (spawnCount / 3) + addend; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(1);
            }

            direction = Random.Range(0, 1);

            if (remainder > 0)
                addend = 1;
            else addend = 0;

            if (direction == 0)
            {
                bottomArrow.SetActive(true);

                for (int i = 0; i < (spawnCount / 3) + addend; i++)
                {
                    GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                    //GO.transform.position = RandomCircle();
                    GO.transform.position = RandomAtDirection(2);
                }
            }
            else
            {
                topArrow.SetActive(true);

                for (int i = 0; i < (spawnCount / 3) + addend; i++)
                {
                    GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                    //GO.transform.position = RandomCircle();
                    GO.transform.position = RandomAtDirection(3);
                }
            }
        }
        else
        {
            topArrow.SetActive(true);
            bottomArrow.SetActive(true);

            for (int i = 0; i < spawnCount / 3; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(2);
            }

            if (remainder > 0)
            {
                addend = 1;
                remainder -= 1;
            }
            else addend = 0;

            for (int i = 0; i < (spawnCount / 3) + addend; i++)
            {
                GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                //GO.transform.position = RandomCircle();
                GO.transform.position = RandomAtDirection(3);
            }

            direction = Random.Range(0, 1);

            if (remainder > 0)
                addend = 1;
            else addend = 0;

            if (direction == 0)
            {
                leftArrow.SetActive(true);

                for (int i = 0; i < (spawnCount / 3) + addend; i++)
                {
                    GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                    //GO.transform.position = RandomCircle();
                    GO.transform.position = RandomAtDirection(0);
                }
            }
            else
            {
                rightArrow.SetActive(true);

                for (int i = 0; i < (spawnCount / 3) + addend; i++)
                {
                    GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
                    //GO.transform.position = RandomCircle();
                    GO.transform.position = RandomAtDirection(1);
                }
            }
        }
    }

    private void SpawnFourDirection()
    {
        rightArrow.SetActive(true);
        leftArrow.SetActive(true);
        topArrow.SetActive(true);
        bottomArrow.SetActive(true);

        GameObject GO;

        int remainder = spawnCount % 4;
        int addend = 0;

        for (int i = 0; i < spawnCount / 4; i++)
        {
            GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
            //GO.transform.position = RandomCircle();
            GO.transform.position = RandomAtDirection(0);
        }

        if (remainder > 0)
        {
            addend = 1;
            remainder -= 1;
        }
        else addend = 0;

        for (int i = 0; i < (spawnCount / 4) + addend; i++)
        {
            GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
            //GO.transform.position = RandomCircle();
            GO.transform.position = RandomAtDirection(1);
        }

        if (remainder > 0)
        {
            addend = 1;
            remainder -= 1;
        }
        else addend = 0;

        for (int i = 0; i < (spawnCount / 4) + addend; i++)
        {
            GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
            //GO.transform.position = RandomCircle();
            GO.transform.position = RandomAtDirection(2);
        }

        if (remainder > 0)
            addend = 1;
        else addend = 0;

        for (int i = 0; i < (spawnCount / 4) + addend; i++)
        {
            GO = enemyPools[enemyType[Random.Range(0, enemyType.Count)]].GetObject();
            //GO.transform.position = RandomCircle();
            GO.transform.position = RandomAtDirection(3);
        }
    }

    public void KilledEnemy()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            waveOngoing = false;

            /*currentTimerForNextSpawn = 0;
            timerForNextSpawn = Random.Range(minTimer, maxTimer);

            waveSlider.minValue = 0;
            waveSlider.maxValue = timerForNextSpawn;*/

            sliderAnimator.SetTrigger("WaveEnd");

            AdvanceToNextWave();

            buildingUnlock.Raise();
        }
        else waveMessage.text = remainingEnemies + " Enemies Remaining";
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

    Vector3 RandomCircle(float radius)
    {
        float angle = Random.value * 360;
        Vector3 pos = Vector3.zero;

        pos.x = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = 0f;
        pos.z = radius * Mathf.Cos(angle * Mathf.Deg2Rad);

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
                delta.z = spawnDistanceFromBase + randomLength;
                delta.x = randomWidth;
                break;
        }

        return pos + delta;
    }

    public void AdvanceToNextWave()
    {
        if (waves.Count > 1)
        {
            if (startEnemyWaveSpawning) waves.RemoveAt(0);
            else startEnemyWaveSpawning = true;

            waveSlider.gameObject.SetActive(true);

            spawnCount = waves[0].spawnNumber;

            minTimer = waves[0].initialMinTimer;
            maxTimer = waves[0].initialMaxTimer;

            enemyType.Clear();

            spawnDirection = waves[0].spawnDirections;

            for (int i = 0; i < waves[0].enemyTypes.Length; i++)
                enemyType.Add(waves[0].enemyTypes[i]);

            currentTimerForNextSpawn = 0;
            timerForNextSpawn = Random.Range(minTimer, maxTimer);
            timerInteger = (int)timerForNextSpawn - 1;

            waveSlider.minValue = 0;
            waveSlider.maxValue = timerForNextSpawn;
        }
        else
        {
            if (!mainGameWon)
            {
                mainGameWon = true;

                winningPanel.SetActive(true);

                Time.timeScale = 0;

                spawnCount = 50;

                minTimer = 15;
                maxTimer = 15;

                enemyType.Clear();

                for (int i = 0; i < 7; i++)
                    enemyType.Add(i);

                spawnDirection = 4;
            }
            else spawnCount += 5;

            waveSlider.gameObject.SetActive(true);

            currentTimerForNextSpawn = 0;
            timerForNextSpawn = Random.Range(minTimer, maxTimer);
            timerInteger = (int)timerForNextSpawn - 1;

            waveSlider.minValue = 0;
            waveSlider.maxValue = timerForNextSpawn;
        }
    }

    [ContextMenu("Organize Waves")]
    public void OrganizeWaves()
    {
        waves = waves.OrderBy(e => e.waveNumber).ToList();

        Debug.Log("Finished Organizing Waves");
    }

    public void SpawnEnemy(int type, Vector3 position)
    {
        GameObject temp = enemyPools[type].GetObject();
        temp.transform.position = position;
        remainingEnemies++;
    }

    public void SpawnEnemy(int type, Vector3 position, float radius)
    {
        GameObject temp = enemyPools[type].GetObject();
        temp.transform.position = position + RandomCircle(radius);
        remainingEnemies++;
    }

    IEnumerator WarningAnimation()
    {
        float currentTicks = 0;
        float flashTime = 0.5f;

        for(int i = 0; i < 3; i++)
        {
            while (currentTicks < flashTime)
            {
                currentTicks += Time.deltaTime;

                if (currentTicks > flashTime) currentTicks = flashTime;

                arrow.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, currentTicks / flashTime));

                yield return null;
            }

            while (currentTicks > 0)
            {
                currentTicks -= Time.deltaTime;

                if (currentTicks < 0) currentTicks = 0;

                arrow.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, currentTicks / flashTime));

                yield return null;
            }

            yield return new WaitForSeconds(0.25f);
        }

        topArrow.SetActive(false);
        bottomArrow.SetActive(false);
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
    }
}