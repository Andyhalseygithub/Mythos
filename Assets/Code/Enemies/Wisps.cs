using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisps : Entity
{
    // Start is called before the first frame update
    //float acceleration = 1f;
    Rigidbody2D _rigidbody2D;
    Transform target;
    SpriteRenderer _spriteRenderer;
    Vector2 moveDirection;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //target = GameObject.FindWithTag("Player").transform;
        damage = 15;
        speed = 7.5f;
        health = 200;
        //target = transform.LookAt(Player);
        //target = FindObjectOfType<Playercontrol>().transform;
        //target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        /*
        float acceleration = 1f;
        float maxSpeed = 2f;
        //ChaseAI();
        if(target != null){
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            _rigidbody2D.MoveRotation(angle);
        }
        _rigidbody2D.AddForce(transform.right * acceleration);
        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, maxSpeed);
        */

        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody2D.rotation = angle;
            moveDirection = direction;

            _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        }
        if (transform.position.x > target.position.x)
        {
            _spriteRenderer.flipX = true;
            //print("Right");
        }
        else if (transform.position.x < target.position.x)
        {
            _spriteRenderer.flipX = false;
            //print("Left");
        }

    }
    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())
        {
            health -= other.gameObject.GetComponent<baseprojectile>().damage;
            if (health < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())
        {
            health -= other.gameObject.GetComponent<baseprojectile>().damage;
            if (health < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
