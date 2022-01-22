using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //dontdestroy
    public static DontDestroy Instance;

    private void Awake()
    {
        if (DontDestroy.Instance == null)
        {
            DontDestroy.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
