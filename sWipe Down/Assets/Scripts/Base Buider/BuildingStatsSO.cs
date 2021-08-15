using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building Stats", fileName = "New Building Stats", order = 0)]
public class BuildingStatsSO : ScriptableObject
{
    public int health;
    public float fireRate;
    public float actionValue;
}
