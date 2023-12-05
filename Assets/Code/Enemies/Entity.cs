using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    public float speed;
    public float damage;
    public bool ranDeathFunction;
    
    //public float iframes;
    void Start()
    {
        ranDeathFunction = false;
    }

    // Update is called once per frame
    void Update()
    {
   /*     if (iframes > 0)
        {
            iframes -= 1000f * Time.deltaTime;
        }
        if (iframes < 0)
        {
            iframes = 0;
        }*/
    }
    public void death(int spiritsGained)
    {
        if (ranDeathFunction == false)
        {
            GameController.instance.GetSpirits(spiritsGained);
            Destroy(gameObject);
            ranDeathFunction = true;
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        //iframes = 1000f;
        if (health < 0)
        {
            death(10);
        }
    }
}
