using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpecifier : MonoBehaviour
{
    [SerializeField] private bool androidSpecific = false;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        if (!androidSpecific) gameObject.SetActive(false);
#else
        if (androidSpecific) gameObject.SetActive(false);
#endif
    }
}
