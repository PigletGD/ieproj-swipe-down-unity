using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Milestone", fileName = "New Milestone", order = 0)]
public class MilestoneSO : ScriptableObject
{
    public int valueThreshold;
    public VoidEvent milestoneType;
}