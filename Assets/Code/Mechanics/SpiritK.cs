using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritK : MonoBehaviour
{
    public static SpiritK instance;
    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        gameObject.SetActive(true);
    }

    public void toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
