using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class annihilationray : baseprojectile
{
    // Start is called before the first frame update
    Rigidbody2D _rigidbody2D;
    public GameObject annihilationexplosion;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = -transform.right * 10f;
        damage = 250;
        //_rigidbody2D.AddRelativeForce(Vector2.right * 10f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject newProjectile = Instantiate(annihilationexplosion);
        newProjectile.transform.position = transform.position;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity += _rigidbody2D.velocity * 1.1f * Time.deltaTime;
    }
}
