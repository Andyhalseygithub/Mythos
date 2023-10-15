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
    public float shoottimer = 10000;
    public GameObject Shogunslash;
    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").transform;
        damage = 70;
        health = 5000;
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position + new Vector3(0, 5, 0) - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody2D.rotation = angle;
            moveDirection = direction;

            _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        }
        if(transform.position.x > target.position.x)
        {
            _spriteRenderer.flipX = true;
            //print("Right");
        }
        else if(transform.position.x < target.position.x)
        {
            _spriteRenderer.flipX = false;
            //print("Left");
        }
        //var DirToPlayer = target.transform.position - this.transform.position;
        //var scaleX = DirToPlayer.x < 0f? -5f : 5f;
        //this.transform.localScale = new Vector3(scaleX, 5f, 5f);

        shoottimer -= 10000f * Time.deltaTime;
        if (health >= 2500 && shoottimer <= 0)
        {
            {
                {
                    GameObject newProjectile = Instantiate(Shogunslash);
                    newProjectile.transform.position = transform.position;
                    newProjectile.transform.rotation = Quaternion.Euler(0, 0, 90);
                    GameObject newProjectile2 = Instantiate(Shogunslash);
                    newProjectile2.transform.position = transform.position;
                    newProjectile2.transform.rotation = Quaternion.Euler(0, 0, 130);
                    GameObject newProjectile3 = Instantiate(Shogunslash);
                    newProjectile3.transform.position = transform.position;
                    newProjectile3.transform.rotation = Quaternion.Euler(0, 0, 50);
                    shoottimer = 10000;
                }
            }
        }

        if (health <= 2500 && shoottimer <= 0)
        {
            {
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
                    shoottimer = 5000;
                }
            }
        }

        /*
        if (shoottimer == 5000)
        {
            {
                {
                    GameObject newProjectile = Instantiate(Shogunslash);
                    newProjectile.transform.position = transform.position;
                    newProjectile.transform.rotation = Quaternion.Euler(0, 0, 110);
                }
            }
        }

        if (shoottimer == 10000)
        {
            {
                {
                    GameObject newProjectile = Instantiate(Shogunslash);
                    newProjectile.transform.position = transform.position;
                    newProjectile.transform.rotation = Quaternion.Euler(0, 0, 70);
                }
            }
        }

        if (shoottimer == 15000)
        {
            {
                {
                    GameObject newProjectile = Instantiate(Shogunslash);
                    newProjectile.transform.position = transform.position;
                    newProjectile.transform.rotation = Quaternion.Euler(4, -2, 90);
                }
            }
        }

        if (shoottimer == 20000)
        {
            {
                {
                    GameObject newProjectile = Instantiate(Shogunslash);
                    newProjectile.transform.position = transform.position;
                    newProjectile.transform.rotation = Quaternion.Euler(-4, -2, 90);
                }
            }
        }
        */
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
