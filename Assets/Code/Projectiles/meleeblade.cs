using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeblade : baseprojectile
{
    Rigidbody2D _rigidbody2D;
    public float timer = 1000;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * -30f;
        damage = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 10000f * Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
