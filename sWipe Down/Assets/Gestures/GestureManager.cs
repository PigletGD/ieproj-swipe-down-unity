using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;

    public TapProperty _tapProperty;
    public event EventHandler<TapEventArgs> onTap;
    private Touch gestureFinger1;
    
    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0;
    private bool isTouching = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            gestureFinger1 = Input.GetTouch(0);

            if (gestureFinger1.phase == TouchPhase.Began)
            {
                startPoint = gestureFinger1.position;
                gestureTime = 0;
            }

            if (gestureFinger1.phase == TouchPhase.Ended)
            {
                endPoint = gestureFinger1.position;
                if(gestureTime <= _tapProperty.tapTime && Vector2.Distance(startPoint,endPoint) < Screen.dpi * _tapProperty.tapMaxDistance)
                {
                   FireTapEvent(startPoint);
                }
            }
            else
            {
                gestureTime += Time.deltaTime;
            }
        }
    }

    private void FireTapEvent(Vector2 pos)
    {
        //Debug.Log("Tap!");
        if (onTap != null)
        {
            TapEventArgs tapArgs = new TapEventArgs(pos);
            onTap(this, tapArgs);
        }
    }
}
