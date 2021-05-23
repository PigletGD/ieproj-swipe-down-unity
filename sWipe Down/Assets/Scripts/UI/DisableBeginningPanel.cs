using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBeginningPanel : MonoBehaviour
{
    float timeElapsed = 0;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= 1.1f)
        {
            gameObject.SetActive(false);
        }
    }
}
