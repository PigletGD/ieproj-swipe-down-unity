using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    [SerializeField] TowerBehaviour tower = null;
    [SerializeField] Transform towerTransform = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            tower.AddTarget(other.transform);
            other.GetComponent<EnemyMove>().targetedList.Add(towerTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            tower.RemoveTarget(other.transform);
            other.GetComponent<EnemyMove>().targetedList.Remove(towerTransform);
        }
    }
}
