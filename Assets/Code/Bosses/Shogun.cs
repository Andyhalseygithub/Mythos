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
    Collider2D collid;
    Vector2 moveDirection;
    public float shoottimer;
    public float shootcounter;
    public float randomdirection;
    public GameObject Shogunslash;
    public GameObject spiritblast;
    public GameObject trace;
    public GameObject upsword;
    public GameObject downsword;
    public float counter;
    public bool move;
    public bool move2;
    public bool move3;
    public float randomdist;
    public float randomAngle;
    public float randomAngle2;
    public float randomAngle3;
    public UnityEngine.UI.Image BossHealthBar;
    public UnityEngine.UI.Image BossHealthBarBack;
    public float maxHealth;
    Vector3 previousPosition;
    //public GameObject target;
    public bool start;
    public bool bottomofscreen;
    public float despcounter;
    public bool prerain;
    public bool rain;
    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        damage = 150;
        health = 6500;
        maxHealth = 6500;
        move = false;
        move2 = false;
        move3 = false;
        start = false;
        bottomofscreen = false;
        rain = false;
        prerain = false;
        StartCoroutine(Wait());
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
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
        if (health < 0)
        {
            MenuController.instance.disablebosshealth();
            MusicController.instance.playworldtheme();
            death(10000);
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            damage = 0;
            Vector3 direction = (target.position + new Vector3(0, 7, 0) - transform.position).normalized;
            moveDirection = direction;

            _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
        if (move2)
        {
            damage = 0;
            Vector3 direction = (target.position + new Vector3(0, -7, 0) - transform.position).normalized;
            moveDirection = direction;

            _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
        if (move3)
        {
            damage = 0;
            Vector3 direction = (target.position + new Vector3(0, 4, 0) - transform.position).normalized;
            moveDirection = direction;

            _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
        BossHealthBar.fillAmount = health / maxHealth;
    }

    IEnumerator PreCharge()
    {
        if (health < 2500)
        {
            yield return StartCoroutine(Desperation());
        }
        //yield return new WaitForSeconds(0.50f);
        damage = 0;
        speed = 25;
        target = GameObject.FindWithTag("Player").transform;
        if (counter < 100)
        {
            if (randomdirection == 1)
            {
                Vector3 direction = (target.position + new Vector3(-6, 0, 0) - transform.position).normalized;
                //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //_rigidbody2D.rotation = angle;
                moveDirection = direction;

                _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
            }
            if (randomdirection == 2)
            {
                Vector3 direction = (target.position + new Vector3(6, 0, 0) - transform.position).normalized;
                //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //_rigidbody2D.rotation = angle;
                moveDirection = direction;

                _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
            }
            counter += 1;
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(PreCharge());
        }
        else
        {
            counter = 0;
            yield return StartCoroutine(Charge());
        }
    }
    IEnumerator Charge()
    {
        if (health < 2500)
        {
            yield return StartCoroutine(Desperation());
        }
        Audiocontroller.instance.playcharge();
        speed = 30;
        damage = 150;
        target = GameObject.FindWithTag("Player").transform;
        if (randomdirection == 1)
        {
        Vector3 direction = (target.position + new Vector3(6, 0, 0) - transform.position).normalized;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //_rigidbody2D.rotation = angle;
        moveDirection = direction;

        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
        if (randomdirection == 2)
        {
        Vector3 direction = (target.position + new Vector3(-6, 0, 0) - transform.position).normalized;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //_rigidbody2D.rotation = angle;
        moveDirection = direction;

        _rigidbody2D.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
    }
        yield return new WaitForSeconds(0.50f);
        yield return StartCoroutine(SpiritBlast());
    }
    IEnumerator Shoot()
    {
        if (health < 2500)
        {
            yield return StartCoroutine(Desperation());
        }
        damage = 0;
        speed = 10;
        target = GameObject.FindWithTag("Player").transform;
        Vector3 direction = (target.position + new Vector3(0, 6, 0) - transform.position).normalized;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //_rigidbody2D.rotation = angle;
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
            yield return new WaitForSeconds(0.5f);
            shoottimer = 0;
            yield return StartCoroutine(PreCharge());
        }
    }

    IEnumerator Wait()
    {
        damage = 0;
        yield return new WaitForSeconds(1f);
        MenuController.instance.Showbosshealth();
        MusicController.instance.playbosstheme();
        yield return StartCoroutine(Shoot());
    }

    IEnumerator SpiritBlast()
    {
        if (health < 2500)
        {
            yield return StartCoroutine(Desperation());
        }
        damage = 0;
        if (shootcounter < 20)
        {
            if (shootcounter == 0)
            {
                move = true;
                yield return new WaitForSeconds(1f);
            }
            //yield return new WaitForSeconds(.5f);
            randomdist = Random.Range(-3, 3);
            randomAngle = Random.Range(-.5f, -2.5f);
            float angle = randomAngle * Mathf.Rad2Deg;
            GameObject newProjectile = Instantiate(trace);
            newProjectile.transform.position = transform.position; //new Vector3(transform.position.x + randomdist, transform.position.y, 0);
            previousPosition = transform.position;
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return new WaitForSeconds(.50f);
            Audiocontroller.instance.playshoot();
            GameObject newProjectile2 = Instantiate(spiritblast);
            newProjectile2.transform.position = previousPosition; //new Vector3(transform.position.x + randomdist, transform.position.y, 0);
            newProjectile2.transform.rotation = Quaternion.Euler(0, 0, angle);
            shootcounter += 1;
            yield return new WaitForSeconds(.2f);
            yield return StartCoroutine(SpiritBlast());
        }
        if (shootcounter == 20)
        {
            yield return new WaitForSeconds(1f);
            move = false;
            shootcounter = 0;
            yield return StartCoroutine(Shoot());            
        }
            /*move = true;
            yield return new WaitForSeconds(.25f);
            randomdist = Random.Range(-3, 3);
            randomAngle = Random.Range(-50f, 50f);
            float angle = randomAngle * Mathf.Rad2Deg;
            GameObject newProjectile = Instantiate(spiritblast);
            newProjectile.transform.position = transform.position; //new Vector3(transform.position.x + randomdist, transform.position.y, 0);
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, 0);
            GameObject newProjectile2 = Instantiate(spiritblast);
            newProjectile.transform.position = transform.position + new Vector3(transform.position.x - randomdist, transform.position.y, 0);
            newProjectile2.transform.rotation = Quaternion.Euler(5, -5, 90 - angle);
            GameObject newProjectile3 = Instantiate(spiritblast);
            newProjectile.transform.position = transform.position + new Vector3(transform.position.x - randomdist, transform.position.y, 0);
            newProjectile3.transform.rotation = Quaternion.Euler(10, 10, angle);
            shootcounter += 1;
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(SpiritBlast());
            if (shootcounter == 30)
            {
                yield return new WaitForSeconds(1f);
                move = false;
                shootcounter = 0;
                yield return StartCoroutine(Shoot());
            }*/
        }
/*    IEnumerator track()
    {
        Vector3 direction = (target.position + new Vector3(0, 7, 0) - transform.position).normalized;
        yield return new WaitForSeconds(0.01f);
        yield return StartCoroutine(spiritblast());
    }*/

    IEnumerator Desperation()
    {
        damage = 0;
        if (start == false) {
            move2 = true;
            Audiocontroller.instance.playphase();
            yield return new WaitForSeconds(2f);
            bottomofscreen = true;
            start = true;
            yield return StartCoroutine(Desperation());
        }
        if (bottomofscreen == true)
        {
            if (despcounter < 101 && bottomofscreen == true) {
                randomAngle = Random.Range(2f, 4.5f);
                float angle = randomAngle * Mathf.Rad2Deg;
                randomAngle2 = Random.Range(.5f, 2.5f);
                float angle2 = randomAngle * Mathf.Rad2Deg;
                randomAngle3 = Random.Range(.5f, 2.5f);
                float angle3 = randomAngle * Mathf.Rad2Deg;
                Audiocontroller.instance.playthrow();
                GameObject newProjectile = Instantiate(upsword);
                newProjectile.transform.position = transform.position; //new Vector3(transform.position.x + randomdist, transform.position.y, 0);
                newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
                /*GameObject newProjectile2 = Instantiate(trace);
                newProjectile.transform.position = transform.position; // + new Vector3(transform.position.x - randomdist, transform.position.y, 0);
                newProjectile2.transform.rotation = Quaternion.Euler(0, 0, angle2);
                GameObject newProjectile3 = Instantiate(trace);
                newProjectile.transform.position = transform.position; // + new Vector3(transform.position.x - randomdist, transform.position.y, 0);
                newProjectile3.transform.rotation = Quaternion.Euler(0, 0, angle3);*/
                despcounter += 1;
                if (despcounter > 100)
                {
                    bottomofscreen = false;
                    prerain = true;
                }
                yield return new WaitForSeconds(0.01f);
                yield return StartCoroutine(Desperation());
            }
        }
        if(prerain == true)
        {
            yield return new WaitForSeconds(4f);
            prerain = false;
            rain = true;
            move2 = false;
            move3 = true;
            yield return StartCoroutine(Desperation());
        }
        if (rain == true)
        {
            float angle = 0.11f * Mathf.Rad2Deg;
            float angle2 = -1.5f * Mathf.Rad2Deg;
            float rand = Random.Range(-12, 12);
            Audiocontroller.instance.playfallsword();
            GameObject newProjectile = Instantiate(downsword);
            Vector3 location = (target.position + new Vector3(rand, 12, 0));
            Vector3 location2 = (target.position + new Vector3(rand, 7, 0));
            newProjectile.transform.position = location; //new Vector3(transform.position.x + randomdist, transform.position.y, 0);
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
            GameObject newProjectile2 = Instantiate(trace);
            newProjectile2.transform.position = location2; //new Vector3(transform.position.x + randomdist, transform.position.y, 0);
            newProjectile2.transform.rotation = Quaternion.Euler(0, 0, angle2);
            yield return new WaitForSeconds(0.30f);
            yield return StartCoroutine(Desperation());
        }
    }
    public void activate()
    {
        gameObject.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<baseprojectile>())
        {
            health -= other.gameObject.GetComponent<baseprojectile>().damage;
            if (health < 0)
            {
                death(10000);
                MenuController.instance.disablebosshealth();
                MusicController.instance.playworldtheme();
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
                MusicController.instance.playworldtheme();
                MenuController.instance.disablebosshealth();
            }
        }
    }
}
