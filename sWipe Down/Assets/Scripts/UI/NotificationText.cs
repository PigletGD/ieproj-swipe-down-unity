using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationText : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.enabled = false;
    }

    public void NotifyPlayerBuildMode()
    {
        StopAllCoroutines();

        text.enabled = true;

        text.text = "Tower Build Mode Enabled";

        StartCoroutine("Timer");
    }

    public void NotifyPlayerMoveMode()
    {
        StopAllCoroutines();

        text.enabled = true;

        text.text = "Camera Move Mode Enabled";

        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);

        text.enabled = false;
    }
}
