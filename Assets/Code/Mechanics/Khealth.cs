using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Khealth : MonoBehaviour
{
    public static Khealth instance;
    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        gameObject.SetActive(true);
    }

    public void on()
    {
        gameObject.SetActive(true);
    }
    public void off()
    {
        gameObject.SetActive(false);
    }
}
