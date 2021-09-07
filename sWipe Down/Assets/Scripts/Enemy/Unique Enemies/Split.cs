using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour, IDeathHandler
{
    [SerializeField] GameObject offspring;
    [SerializeField] int offspringCount;
    public void OnDeath()
    {
        for (int i = 0; i < offspringCount; i++)
        {

        }
    }
}
