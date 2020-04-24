using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BootStrap : MonoBehaviour
{
    public bool IsLoadWorld;

    // Start is called before the first frame update
    void Awake()
    {
        IsLoadWorld = false;
        DontDestroyOnLoad(this.gameObject);
    }

    public void IsLoadWorldTrue()
    {
        IsLoadWorld = true;
        Serialization serialization = Serialization.GetInstance();
        serialization.Load();
    }
}
