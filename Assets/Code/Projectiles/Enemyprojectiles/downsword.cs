using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downsword : Entity
{
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    float randomAngle;
    float startangle;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //randomSpeed = Random.Range(10f, 20f);
        //randomAngle = Random.Range(-0.1f, .1f);
        //StartCoroutine(GetStartAngle());
        damage = 50;
    }
    //                                 |
    //  AWESOME MAGIC/LIGHTNING EFFECT \/
    void FixedUpdate()
    {
        /*randomAngle = Random.Range(-50f, 50f);
        float angle = randomAngle * Mathf.Rad2Deg;
        _rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, (-90 - randomAngle));
        _rigidbody2D.velocity = transform.right * randomSpeed;*/
        _rigidbody2D.velocity = -transform.up * 20f;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
