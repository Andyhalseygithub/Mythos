using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritS : MonoBehaviour
{
    public static SpiritS instance;
    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
