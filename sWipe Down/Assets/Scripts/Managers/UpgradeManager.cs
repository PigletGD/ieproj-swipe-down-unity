using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatsPair
{
    public BuildingStatsSO current;
    public BuildingStatsSO initial;
}

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] StatsPair[] statPairs;

    // Start is called before the first frame update
    void Start()
    {
        ResetStats();
    }


    private void ResetStats()
    {
        foreach (StatsPair stats in statPairs)
        {
            stats.current.health = stats.initial.health;
            stats.current.fireRate = stats.initial.fireRate;
            stats.current.actionValue = stats.initial.actionValue;
        }
    }

    public void IncreaseBidetFireRate(int value)
    {
        statPairs[1].current.fireRate /= value;
    }

    public void IncreaseTPGenerationValue(int value)
    {
        statPairs[4].current.actionValue += value;
    }
}