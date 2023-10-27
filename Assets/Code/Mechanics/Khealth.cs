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

    public void toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
        /*if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
        else if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(gameObject.activeInHierarchy);
        }*/
    }
}
