using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPGenerationBehaviour : TowerBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private int basegain = 10;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    public override void ExecuteAction()
    {
        manager.IncrementScore(basegain);
    }

    public override bool ReadyToExecuteAction()
    {
        if (time >= fireRate) return true;
        return false;
    }
}
