using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBehaviour : MonoBehaviour
{
    public Transform target = null;
    public List<Transform> targetList = null;
    public List<Transform> targetedList = null;

    protected float time = 0.0f;
    public float fireRate = 1.0f;

    public string key = "";

    protected GameManager manager = default;

    [SerializeField] private int towerValue = default;

    void Start()
    {
        targetList = new List<Transform>();
        targetedList = new List<Transform>();

        manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void Update() => ReadyAction();

    public abstract void ExecuteAction();

    public abstract bool ReadyToExecuteAction();

    public void ReadyAction()
    {
        time += Time.deltaTime;
        if (!ReadyToExecuteAction()) return;
        ExecuteAction();
        time = 0;
    }

    public void AddTarget(Transform foundTarget)
    {
        if (targetList.Count <= 0) target = foundTarget;

        targetList.Add(foundTarget);
    }

    public void RemoveTarget(Transform lostTarget)
    {
        targetedList.Remove(lostTarget);
        targetList.Remove(lostTarget);

        if (targetList.Count > 0) target = targetList[0];
        else target = null;
    }

    public void RemoveTargetedList()
    {
        foreach (Transform targeted in targetedList)
        {
            if (targeted != null)
            {
                EnemyMove em = targeted.GetComponent<EnemyMove>();
                if (em != null) em.RemoveTarget(transform);
            }
        }
    }

    public void ReduceTotalValue()
    {
        manager.DecrementScore(towerValue);
    }

    public void SetTowerValue(int value)
    {
        towerValue = value;
    }
}
