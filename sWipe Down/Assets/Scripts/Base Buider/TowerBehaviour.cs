using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public Transform target =  null;
    public List<Transform> targetList = null;
    public List<Transform> targetedList = null;
    public bool isAttackTower;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnpoint;
    private float time = 0.0f;
    private int firerate = 1;

    public string key = "";
    // Start is called before the first frame update

    void Start()
    {
        targetList = new List<Transform>();
        targetedList = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttackTower)
        {
            time += Time.deltaTime;

            if (target != null)
            {
                Vector3 direction = new Vector3(target.position.x, 0f, target.position.z);
                transform.LookAt(direction);
                if (time >= firerate)
                {
                    time = 0;
                    GameObject temp = Instantiate(bullet, spawnpoint.transform.position, Quaternion.identity);
                    temp.transform.LookAt(direction);
                }
            }
        }
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
}
