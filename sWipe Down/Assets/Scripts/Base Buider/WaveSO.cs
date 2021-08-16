using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave", fileName = "New Wave", order = 0)]
public class WaveSO : ScriptableObject
{
    public int waveNumber;
    public int[] enemyTypes;
    public int spawnNumber;
    public int initialMinTimer;
    public int initialMaxTimer;
}