using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeblade : baseprojectile
{
    Transform transformforscale;
    Rigidbody2D _rigidbody2D;
    public float timer = 2500;
    public float size;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        transformforscale = GetComponent<Transform>();
        _rigidbody2D.velocity = transform.right * 30f;
        damage = 10;
        size = 1;
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
    void FixedUpdate()
    {
        size += size / 1.01f * Time.deltaTime;
        transformforscale.localScale = new Vector3(2, 2 + size, 2);
    }
}
