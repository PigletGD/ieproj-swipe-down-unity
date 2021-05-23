using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building", fileName = "New Building", order = 0)]
public class BuildingSO : ScriptableObject
{
    public GameObject buildingPrefab;
    public int buildingCost;
    public Sprite buildingIcon;
}
