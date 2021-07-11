using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TapEventArgs : EventArgs
{
    private Vector2 _tapPosition;

    public TapEventArgs(Vector2 pos)
    {
        _tapPosition = pos;
    }

    public Vector2 TapPosition
    {
        get
        {
            return _tapPosition;
        }
    }
}
