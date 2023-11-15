using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.Mathematics;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Knight : Playerbase
{
    //animation
    Animator animator;
    //Initialize playercontrol as an object for enemies to track
    public static Knight instance;
    // initialize health and spirit vars
    public TMP_Text thealth;
    public float health;
    public float maxHealth;
    public TMP_Text tspirit;
    public float spirit;
    public float spiritMax;
    public UnityEngine.UI.Image Spiritbar;
    public bool freezeflag;
    // inventory variables 
    public float weapon_equipped = 1;
    // Initializing jumps and flighttime vars
    public int jump_counter;
    public float flight_time;
    // Initialize rigidbody for usage in code
    Rigidbody2D _rigidbody2D;
    //get transform and projectile code for player usage
    public Transform aimPivot;
    public GameObject testprojectilePrefab;
    public GameObject playershogunswordprefab;
    public GameObject meleeblade;
    public GameObject annihilationray;
    public GameObject Spiritslasher;
    public GameObject Lightningprefab;
    public GameObject Cataclysmprefab;
    public GameObject Penumbraprefab;
    // Rotation timer
    public float i = 100;

    // Inventory
    public Item[] inventory;
    public int activeslot;
    //Health
    public UnityEngine.UI.Image HealthBar;
    //Invincibility frames
    public float iframes;

    //Speed and acceleration
    public float speedX = 60f;
    public float maxSpeedX = 20f;
    public float speedY = 200f;
    public float maxSpeedY = 20f;

    // Call sprite renderer
    SpriteRenderer[] _spriteRenderer;

    //public mouse position needed for some weapons in fixed update
    public float angleToMouse;

    //menus
    public bool paused;
    void Awake(){
        instance = this;
    }


    void Start()
    { 
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        health = 20000;
        maxHealth = 20000;
        spirit = 1000;
        spiritMax = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuController.instance.Show();
        }
        if (paused)
        {
            return;
        }
        //health
        //thealth.text = health.ToString();

        /*
        // Set mass to 0.5 or lower for this, modify linear drag
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            _rigidbody2D.AddForce(Vector2.down * 25f * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)
            if (_rigidbody2D.velocity.magnitude > maxSpeedY)
            {
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * maxSpeedY;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.AddForce(Vector2.left * 15f * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            // sprite direction
            if (_rigidbody2D.velocity.magnitude > maxSpeedX)
            {
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * maxSpeedX;
            }
            // sprite direction
            _spriteRenderer.flipX = true;

        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.AddForce(Vector2.right * 15f * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            // sprite direction
            if (_rigidbody2D.velocity.magnitude > maxSpeedX)
            {
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * maxSpeedX;
            }
            _spriteRenderer.flipX = false;
        }*/

        //if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){}

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            _rigidbody2D.AddForce(Vector2.down * speedY * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.AddForce(Vector2.left * speedX * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)

            // sprite direction
            transform.localScale = new Vector3(-2, 2, 2);
            //_spriteRenderer.flipX = true;

        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.AddForce(Vector2.right * speedX * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            // sprite direction
            transform.localScale = new Vector3(2, 2, 2);
            //_spriteRenderer.flipX = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && jump_counter > 0)
        {
            jump_counter--;
            _rigidbody2D.AddForce(Vector2.up * 35f, ForceMode2D.Impulse); // vector(x,y,z)
        }

        // Flight
        /*
        if(Input.GetKey(KeyCode.Space) && jump_counter == 0){
            if(flight_time > 0){
                flight_time--;
                _rigidbody2D.AddForce(Vector2.up * .04f, ForceMode2D.Impulse);

            }
        }
        */
        //Application.targetFrameRate = 60; // Affects the rate of flight
        if (Input.GetKey(KeyCode.Space) && jump_counter == 0)
        {
            if (flight_time > 0)
            {
                flight_time -= 100f * Time.deltaTime;
                _rigidbody2D.AddForce(Vector2.up * 120f * Time.deltaTime, ForceMode2D.Impulse);

            }
        }

        // Aim
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        // Old Attack
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapon_equipped = 0;
        }
        if (weapon_equipped == 0 && Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(testprojectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }


        //New Attack
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            weapon_equipped = 1;
        }
        if (weapon_equipped == 1 && Input.GetMouseButtonDown(0))
        {

            GameObject newProjectile = Instantiate(playershogunswordprefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse + 90);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            weapon_equipped = 2;
        }
        if (weapon_equipped == 2 && Input.GetMouseButton(0))
        {

            GameObject newProjectile = Instantiate(meleeblade);
            newProjectile.transform.position = transform.position;
            i += 5; // Rotation in degrees increment
            if (i > 220)
            {
                i = 100;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);
            //newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - 180);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            weapon_equipped = 3;
        }
        if (weapon_equipped == 3 && Input.GetMouseButton(0))
        {

            GameObject newProjectile = Instantiate(annihilationray);
            newProjectile.transform.position = transform.position;
            //newProjectile.transform.rotation = aimPivot.rotation;
            i += 5; // Rotation in degrees increment
            if (i > 200)
            {
                i = 180;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            weapon_equipped = 4;
        }
        if (weapon_equipped == 4 && Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(Spiritslasher);
            newProjectile.transform.position = transform.position;
            i += 25; // Rotation in degrees increment
            if (i > 225)
            {
                i = 175;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);
        }


        //freeze ability 
        //freeze ability 
        if (Input.GetKey(KeyCode.F) && spirit > 0)
        {
            //_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            //decrement spirit while using ability
            spirit -= 500f * Time.deltaTime;
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            freezeflag = true;
        }
        else if (Input.GetKey(KeyCode.F) && spirit <= 0)
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            freezeflag = false;
        }
        else
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            freezeflag = false;
        }

        // Spirit dash ability
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit == 1000 || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && spirit == 1000)
        {
            _rigidbody2D.AddForce(Vector2.right * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit == 1000 || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && spirit == 1000)
        {
            _rigidbody2D.AddForce(Vector2.left * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit == 1000 || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && spirit == 1000)
        {
            _rigidbody2D.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit == 1000 || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && spirit == 1000)
        {
            _rigidbody2D.AddForce(Vector2.down * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
        }

        // Regenerate spirit
        if (spirit < 0 && freezeflag != true)
        {
            spirit = 0;
        }
        if (spirit <= 1000 && freezeflag != true)
        {
            spirit += 500f * Time.deltaTime;
        }
        if (spirit > 1000)
        {
            spirit = 1000;
        }
        Spiritbar.fillAmount = spirit / spiritMax;

        // Increment Iframes
        if (iframes > 0)
        {
            iframes -= 10000f * Time.deltaTime;
        }
        if (iframes < 0)
        {
            iframes = 0;
        }

        // Inventory
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (inventory[0])
            {
                inventory[0].use();
            }
        }

        // transforming
        if ((Input.GetKeyDown(KeyCode.G)))
        {
            Transformation();
            Khealth.instance.toggle();
            Shealth.instance.toggle();
            SpiritK.instance.toggle();
            SpiritS.instance.toggle();
        }
        // freeze movement
        if (animator.GetBool("activechar") == true)
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // damage indication 
        {
            if (iframes > 0)
            {
                for (int i = 0; i < _spriteRenderer.Length; i++)
                {
                    _spriteRenderer[i].color = Color.red;
                }
            }
            /*if (iframes > 0)
            {
                for (int i = 0; i < _spriteRenderer.Length; i++)
                {
                    Color lightred;
                    lightred = new Color(255, 128, 129, 1);
                    _spriteRenderer[i].color = lightred;
                }
            }*/
            else
            {
                for (int i = 0; i < _spriteRenderer.Length; i++)
                {
                    _spriteRenderer[i].color = Color.white;
                }
            }
        }
    }

    private void FixedUpdate() // framerate locking rapid fire weapons
    {
        /*if (Input.GetKey(KeyCode.Alpha3))
        {
            weapon_equipped = 2;
        }
        if (weapon_equipped == 2 && Input.GetMouseButton(0))
        {

            GameObject newProjectile = Instantiate(meleeblade);
            newProjectile.transform.position = transform.position;
            i += 5; // Rotation in degrees increment
            if (i > 220)
            {
                i = 100;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);
            //newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - 180);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            weapon_equipped = 3;
        }
        if (weapon_equipped == 3 && Input.GetMouseButton(0))
        {

            GameObject newProjectile = Instantiate(annihilationray);
            newProjectile.transform.position = transform.position;
            //newProjectile.transform.rotation = aimPivot.rotation;
            i += 5; // Rotation in degrees increment
            if (i > 200)
            {
                i = 180;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);
        }*/
        if (Input.GetKey(KeyCode.Alpha6))
        {
            weapon_equipped = 5;
        }
        if (weapon_equipped == 5 && Input.GetMouseButton(0))
        {
            GameObject newProjectile = Instantiate(Lightningprefab);
            newProjectile.transform.position = transform.position;
            //newProjectile.transform.rotation = Quaternion.Euler(0, 0, -angleToMouse);
            /*i += 25; // Rotation in degrees increment
            if (i > 225)
            {
                i = 175;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);*/
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            weapon_equipped = 6;
        }
        if (weapon_equipped == 6 && Input.GetMouseButton(0))
        {
            GameObject newProjectile = Instantiate(Cataclysmprefab);
            newProjectile.transform.position = transform.position;
            //newProjectile.transform.rotation = Quaternion.Euler(0, 0, -angleToMouse);
            /*i += 25; // Rotation in degrees increment
            if (i > 225)
            {
                i = 175;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);*/
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            weapon_equipped = 7;
        }
        if (weapon_equipped == 7 && Input.GetMouseButton(0))
        {
            GameObject newProjectile = Instantiate(Penumbraprefab);
            newProjectile.transform.position = transform.position;
            //newProjectile.transform.rotation = Quaternion.Euler(0, 0, -angleToMouse);
            /*i += 25; // Rotation in degrees increment
            if (i > 225)
            {
                i = 175;
            }
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angleToMouse - i);*/
        }
    }
    public void Transformation()
    {
        //GameController.instance.TransformationS();
        animator.SetBool("activechar", true);
    }


    // check player jumping to prevent air jumps

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);
            //Debug.DrawRay(transform.position, Vector2.down * 2f);
            for(int i = 0; i < hits.Length; i++){
                RaycastHit2D hit = hits[i];
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    jump_counter = 1;
                    flight_time = 200f;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Entity>() && iframes <= 0)
        {
            health -= other.gameObject.GetComponent<Entity>().damage;
            iframes = 10000;
            if (health <= 0)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentScene);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Entity>() && iframes <= 0)
        {
            health -= other.gameObject.GetComponent<Entity>().damage;
            HealthBar.fillAmount = health / maxHealth;
            iframes = 10000;
            if (health <= 0)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentScene);
            }
        }
    }
}
