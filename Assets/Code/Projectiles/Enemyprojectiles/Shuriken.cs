using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Entity
{
    Rigidbody2D _rigidbody2D;
    Animator _animator;
    public float timer = 15000;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * 20f;
        damage = 50;
        _animator = GetComponentInChildren<Animator>();
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
    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Playercontrol>())
        {
            Destroy(gameObject);
        }
    }
}
