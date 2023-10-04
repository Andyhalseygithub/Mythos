using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershogunsword : baseprojectile
{
    // Start is called before the first frame update
    Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = -transform.up * 10f;
        damage = 50;
    //_rigidbody2D.AddRelativeForce(Vector2.right * 10f);
}

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
  
    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity += _rigidbody2D.velocity * 2f * Time.deltaTime;
    }
}
