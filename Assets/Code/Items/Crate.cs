using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Crate : Entity
{
    Animator animator;
    //public float health;
    //public bool ranDeathFunction;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        health = 1;
        animator.SetBool("broke", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            death(500);
            animator.SetBool("broke", true);
        }
    }

    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())// && iframes <= 0)
        {
            animator.SetBool("broke", true);
            death(500);
        }
    }
    void OnTriggerEnter2D(Collider2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())// && iframes <= 0)
        {
            animator.SetBool("broke", true);
            death(500);
        }
    }
    /*void death(int spiritsGained)
    {
        if (ranDeathFunction == false)
        {
            GameController.instance.GetSpirits(spiritsGained);
            Destroy(gameObject);
            ranDeathFunction = true;
        }
    }*/
}
