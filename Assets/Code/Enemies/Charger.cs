using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Charger : Entity
{
    // Start is called before the first frame update
    //float acceleration = 1f;
    Rigidbody2D _rigidbody2D;
    Transform target;
    SpriteRenderer _spriteRenderer;
    Vector2 moveDirection;
    public float randY;
    public float randX;
    public float currandY;
    public float currandX;
    public float despawnTimer = 300000;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //target = GameObject.FindWithTag("Player").transform;
        damage = 50;
        speed = 15f;
        health = 200;
        //target = transform.LookAt(Player);
        //target = FindObjectOfType<Playercontrol>().transform;
        //target = GameObject.Find("Player").transform;
        StartCoroutine(precharge());
    }

    private void FixedUpdate()
    {
        despawnTimer -= 10000f * Time.deltaTime;
        if (despawnTimer <= 0)
        {
            death(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            death(300);
        }
        target = GameObject.FindWithTag("Player").transform;
        randY = Random.Range(-8, 8);
        randX = Random.Range(-8, 8);

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
            _rigidbody2D.rotation = -angle;
            //moveDirection = direction;

            //_rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        }
        /*        if (transform.position.x > target.position.x)
                {
                    _spriteRenderer.flipX = true;
                    print("eRight");
                }
                else if (transform.position.x < target.position.x)
                {
                    _spriteRenderer.flipX = true;
                    print("eLeft");
                }*/
    }

    IEnumerator precharge()
    {
        target = GameObject.FindWithTag("Player").transform;
        currandX = randX;
        currandY = randY;
        Vector3 direction2 = (target.position + new Vector3(randX, randY, 0) - transform.position).normalized;
        moveDirection = direction2;
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        Vector3 direction = (target.position - transform.position).normalized;
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Wait());
    }
    IEnumerator charge()
    {
        Audiocontroller.instance.playspiritcharger();
        target = GameObject.FindWithTag("Player").transform;
        Vector3 direction2 = (target.position + new Vector3(-currandX, -currandY, 0) - transform.position).normalized;
        moveDirection = direction2;
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        Vector3 direction = (target.position - transform.position).normalized;
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(precharge());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.50f);
        yield return StartCoroutine(charge());
    }
    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())
        {
            health -= other.gameObject.GetComponent<baseprojectile>().damage;
            if (health < 0)
            {
                death(300);
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
                death(300);
            }
        }
    }
}
