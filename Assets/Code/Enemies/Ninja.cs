using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Ninja : Entity
{
    Rigidbody2D _rigidbody2D;
    Transform target;
    Vector2 moveDirection;
    Animator animator;
    public GameObject Shuriken;
    public float angle;
    public float randomAngle;
    public float iframes;
    public int soundchance;
    public float despawnTimer = 300000;
    SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>(); 
        //target = GameObject.FindWithTag("Player").transform;
        health = 300;
        damage = 20;
        StartCoroutine(Wait());
    }
    void FixedUpdate()
    {
        {
            despawnTimer -= 10000f * Time.deltaTime;
            if (iframes > 0)
            {
                iframes -= 10000f * Time.deltaTime;
            }
            if (iframes < 0)
            {
                iframes = 0;
            }
            
            if(despawnTimer <= 0) {
                death(0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0)
        {
            death(250);
        }
        soundchance = Random.Range(1, 5);
        target = GameObject.FindWithTag("Player").transform;
        if (transform.position.x < target.position.x)
        {
            //transform.localScale = new Vector3(-5, 5, 5);
            _spriteRenderer.flipX = true;
            randomAngle = Random.Range(2f, 4.5f);
            //angle = randomAngle * Mathf.Rad2Deg;
            //print("Right");
        }
        else if (transform.position.x > target.position.x)
        {
            //transform.localScale = new Vector3(5, 5, 5);
            _spriteRenderer.flipX = false;
            angle = -90;
            randomAngle = Random.Range(-2f, -4.5f);
            //angle = randomAngle * Mathf.Rad2Deg;
            //print("Left");
        }
    }
    IEnumerator Jump()
    {
        if (soundchance == 1)
        {
            Audiocontroller.instance.playninja();
        }
        animator.SetBool("Jumped", true);
        target = GameObject.FindWithTag("Player").transform;
        Vector3 direction2 = (target.position + new Vector3(0, 8, 0) - transform.position).normalized;
        moveDirection = direction2;
        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;

        Vector3 direction = (target.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject newProjectile = Instantiate(Shuriken);
        //_rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        newProjectile.transform.position = transform.position;
        newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        yield return new WaitForSeconds(0.50f);
        yield return StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Jump());
    }

    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);
            Debug.DrawRay(transform.position, Vector2.down * .5f);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    animator.SetBool("Jumped", false);
                }
            }
        }
        if (other.gameObject.GetComponent<baseprojectile>() && iframes <= 0)// && iframes <= 0)
        {
            health -= other.gameObject.GetComponent<baseprojectile>().damage;
            iframes = 10000;
            if (health < 0)
            {
                death(1000);
            }
        }
    }
    void OnTriggerStay2D(Collider2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())// && iframes <= 0)
        {
            health -= other.gameObject.GetComponent<baseprojectile>().damage;
            //iframes = 1000f;
            if (health < 0)
            {
                death(250);
            }
        }
    }
}
