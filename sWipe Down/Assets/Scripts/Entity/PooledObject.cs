using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool pool = default;

    public void SetPool(ObjectPool newPool) =>
        pool = newPool;

    public void ReturnObject() =>
        pool.ReturnObject(gameObject);
}
