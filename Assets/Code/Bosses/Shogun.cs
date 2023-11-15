using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shogun : Entity
{
    // Start is called before the first frame update
    Animator animator;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    Transform target;
    Vector2 moveDirection;
    public float shoottimer;
    public float randomdirection;
    public GameObject Shogunslash;
    //public GameObject target;
    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        damage = 70;
        health = 5000;
        //StartCoroutine(Wait());
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        Vector3 direction = (target.position + new Vector3(0, 6, 0) - transform.position).normalized;
        /*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody2D.rotation = angle;*/
        moveDirection = direction;
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        if (transform.position.x > target.position.x)
        {
            //transform.localScale = new Vector3(-5, 5, 5);
            _spriteRenderer.flipX = true;
            //print("Right");
        }
        else if (transform.position.x < target.position.x)
        {
            //transform.localScale = new Vector3(5, 5, 5);
            _spriteRenderer.flipX = false;
            //print("Left");
        }
    }
        
/*    IEnumerator PreCharge()
    {
    target = GameObject.FindWithTag("Player").transform;
    if (randomdirection == 1)
    {
        Vector3 direction = (target.position + new Vector3(-6, 0, 0) - transform.position).normalized;
        *//*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody2D.rotation = angle;*//*
        moveDirection = direction;

        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
    if (randomdirection == 2)
    {
        Vector3 direction = (target.position + new Vector3(6, 0, 0) - transform.position).normalized;
        *//*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody2D.rotation = angle;*//*
        moveDirection = direction;

        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
        yield return new WaitForSeconds(0.01f);
        yield return StartCoroutine(PreCharge());
    }
    IEnumerator Charge()
    {
    target = GameObject.FindWithTag("Player").transform;
        if (randomdirection == 1)
        {
        Vector3 direction = (target.position + new Vector3(6, 0, 0) - transform.position).normalized;
        *//*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody2D.rotation = angle;*//*
        moveDirection = direction;

        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
        if (randomdirection == 2)
        {
        Vector3 direction = (target.position + new Vector3(-6, 0, 0) - transform.position).normalized;
        *//*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody2D.rotation = angle;*//*
        moveDirection = direction;

        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
        yield return new WaitForSeconds(0.50f);
        yield return StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        target = GameObject.FindWithTag("Player").transform;
        Vector3 direction = (target.position + new Vector3(0, 6, 0) - transform.position).normalized;
        *//*float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rigidbody2D.rotation = angle;*//*
        moveDirection = direction;
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        if (shoottimer <= 10)
        {
            GameObject newProjectile = Instantiate(Shogunslash);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = Quaternion.Euler(-5, 5, 90);
            GameObject newProjectile2 = Instantiate(Shogunslash);
            newProjectile2.transform.position = transform.position;
            newProjectile2.transform.rotation = Quaternion.Euler(5, -5, 130);
            GameObject newProjectile3 = Instantiate(Shogunslash);
            newProjectile3.transform.position = transform.position;
            newProjectile3.transform.rotation = Quaternion.Euler(10, 10, 50);
            shoottimer += 1;
            yield return new WaitForSeconds(0.50f);
            yield return StartCoroutine(Shoot());
    }
        else if(shoottimer > 10)
        {
            randomdirection = Random.Range(1, 3);
            yield return new WaitForSeconds(1f);
            shoottimer = 0;
            yield return StartCoroutine(PreCharge());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.50f);
        yield return StartCoroutine(Shoot());
    }*/
    

    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())
        {
            health -= other.gameObject.GetComponent<baseprojectile>().damage;
            if (health < 0)
            {
                death(10000);
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
                death(10000);
            }
        }
    }
}
