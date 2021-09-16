using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] EnemyMove enemy = null;
    [SerializeField] Transform enemyTransform = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Aroma")
        {
            enemy.AddForcedTarget(other.transform);
            other.GetComponent<TowerBehaviour>().targetedList.Add(enemyTransform);
        }
        else if (other.tag == "Building")
        {
            enemy.AddTarget(other.transform);
            other.GetComponent<TowerBehaviour>().targetedList.Add(enemyTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Building" || other.tag == "Aroma")
        {
            enemy.RemoveTarget(other.transform);
            other.GetComponent<TowerBehaviour>().targetedList.Remove(enemyTransform);
        }
    }
}
