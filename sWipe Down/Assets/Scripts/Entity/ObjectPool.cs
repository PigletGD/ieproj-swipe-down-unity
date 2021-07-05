using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject pooledObject = default;
    [SerializeField] private int initialPoolSize = default;

    [SerializeField] private List<GameObject> availablePool = new List<GameObject>();
    [SerializeField] private List<GameObject> usedPool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject GO = Instantiate(pooledObject,
                Vector3.zero, Quaternion.identity, transform);
            GO.AddComponent<PooledObject>().SetPool(this);
            GO.name = "Pooled " + i;
            ReturnObject(GO);
        }
    }

    public GameObject GetObject()
    {
        GameObject GO;
        if (availablePool.Count > 0) GO = availablePool[0];
        else GO = Instantiate(pooledObject, Vector3.zero,
            Quaternion.identity, transform);

        GO.SetActive(true);
        availablePool.Remove(GO);
        usedPool.Add(GO);

        return GO;
    }

    public void ReturnObject(GameObject GO)
    {
        GO.SetActive(false);
        usedPool.Remove(GO);
        availablePool.Add(GO);
    }
}
