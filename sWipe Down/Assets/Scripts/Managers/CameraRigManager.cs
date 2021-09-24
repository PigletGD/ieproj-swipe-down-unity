using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigManager : MonoBehaviour
{
    [SerializeField] private Vector2 xLimit = default;
    [SerializeField] private Vector2 zLimit = default;

    [SerializeField] private MouseManager MM = default;

    private Transform ownerTrans = default;

    private bool atLimit = false;

    private void Start()
    {
        ownerTrans = transform;

        xLimit.x += ownerTrans.position.x;
        xLimit.y += ownerTrans.position.x;

        zLimit.x += ownerTrans.position.z;
        zLimit.y += ownerTrans.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(2)) return;

        atLimit = false;

        Vector3 position = ownerTrans.position;

        if (position.x < xLimit.x)
        {
            atLimit = true;

            position.x = Mathf.Lerp(position.x, xLimit.x, 0.1f);

            if (Mathf.Abs(xLimit.x - position.x) < 0.01f)
                position.x = xLimit.x;
        }
        else if (position.x > xLimit.y)
        {
            atLimit = true;

            position.x = Mathf.Lerp(position.x, xLimit.y, 0.1f);

            if (Mathf.Abs(xLimit.y - position.x) < 0.01f)
                position.x = xLimit.y;
        }

        if (position.z < zLimit.x)
        {
            atLimit = true;

            position.z = Mathf.Lerp(position.z, zLimit.x, 0.1f);

            if (Mathf.Abs(zLimit.x - position.z) < 0.01f)
                position.z = zLimit.x;
        }
        else if (position.z > zLimit.y)
        {
            atLimit = true;

            position.z = Mathf.Lerp(position.z, zLimit.y, 0.1f);

            if (Mathf.Abs(zLimit.y - position.z) < 0.01f)
                position.z = zLimit.y;
        }

        ownerTrans.position = position;

        if (atLimit) MM.newPosition = position;
    }
}