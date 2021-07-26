using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Bardbarian : MonoBehaviour
{
    public int maxHP { get; private set; }
    public int curtainHP { get; private set; }

    private HealthComponent healthComponent;
    private bool rage = false;

    public float rageMoveSpeed = 2.0f;
    public float rageAttackSpeed = 1.0f;

    private void Start()
    {
        healthComponent = gameObject.GetComponent<HealthComponent>();
    }

    private void Update()
    {
        if (!rage)
        {
        }
    }
}
