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
    #region variables
    //animation
    Animator animator;
    //Initialize playercontrol as an object for enemies to track
    public static Knight instance;
    // initialize health and spirit vars
    public TMP_Text thealth;
    public static float health;
    public static float maxHealth;
    public TMP_Text tspirit;
    public float spirit;
    public static float spiritMax;
    public UnityEngine.UI.Image Spiritbar;
    public bool freezeflag;
    // inventory variables 
    public float weapon_equipped = 1;
    // Initializing jumps and flighttime vars
    public int jump_counter;
    public float flight_time;
    public static float flight_new;
    public bool flightcheck;
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
    public GameObject flames;
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

    //upgrade texts
    public TMP_Text healthupgrade;
    public TMP_Text wingupgrade;
    public TMP_Text spiritupgrade;
    public static bool unlockedHellbourne;
    public static bool unlockedShadebringer;

    //dash 
    public bool canDash;

    //aim
    public bool FacingLeft;

    //consume
    public bool canConsume;

    #endregion variables

    void Awake(){
        instance = this;
    }


    void Start()
    { 
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        health = 600;
        maxHealth = 600;
        spirit = 1000;
        spiritMax = 1000;
        canDash = true;
        canConsume = true;
        flightcheck = true;
        unlockedHellbourne = false;
        unlockedShadebringer = false;
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

        #region movement

        float movementSpeed = _rigidbody2D.velocity.magnitude;
        animator.SetFloat("speed", movementSpeed);
        if (movementSpeed > 0.1f)
        {
            animator.SetFloat("movementX", _rigidbody2D.velocity.x);
            animator.SetFloat("movementY", _rigidbody2D.velocity.y);
        }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            _rigidbody2D.AddForce(Vector2.down * speedY * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rigidbody2D.AddForce(Vector2.left * speedX * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)

            // sprite direction
            transform.localScale = new Vector3(-2, 2, 2);
            FacingLeft = true;
            //_spriteRenderer.flipX = true;

        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rigidbody2D.AddForce(Vector2.right * speedX * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            // sprite direction
            transform.localScale = new Vector3(2, 2, 2);
            FacingLeft = false;
            //_spriteRenderer.flipX = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && jump_counter > 0)
        {
            jump_counter--;
            _rigidbody2D.AddForce(Vector2.up * 35f, ForceMode2D.Impulse); // vector(x,y,z)
        }

        if (Input.GetKey(KeyCode.Space) && jump_counter == 0)
        {
            if (flight_time > 0)
            {
                flight_time -= 100f * Time.deltaTime;
                _rigidbody2D.AddForce(Vector2.up * 120f * Time.deltaTime, ForceMode2D.Impulse);
                if (flightcheck)
                {
                    StartCoroutine(flight());
                }
            }
        }
        #endregion movement

#region attacks
        // Aim
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;
        if(directionFromPlayerToMouse.x > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        if (directionFromPlayerToMouse.x < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        //flip mouse on player looking other way
        /*if (FacingLeft)
        {
            Flip();
        }
        else
        if (!FacingLeft)
        {
            Flip();
        }*/

        // Sword Attack
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapon_equipped = 1;
            ShowSword();
        }
       if (weapon_equipped == 1 && armswinging.instance.LaunchProjectile == true)
        {
            GameObject newProjectile = Instantiate(meleeblade);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        //Shadebringer
        if (Input.GetKey(KeyCode.Alpha2) && unlockedShadebringer)
        {
            weapon_equipped = 2;
            ShowShadebringer();
        }
        //Hellbourne
        if (Input.GetKey(KeyCode.Alpha3) && unlockedHellbourne)
        {
            weapon_equipped = 3;
            ShowHellbourne();
        }
        if (weapon_equipped == 3 && armswinging.instance.LaunchProjectile == true)
        {
            GameObject newProjectile = Instantiate(flames);
            newProjectile.transform.position = transform.position;
            GameObject newProjectile2 = Instantiate(flames);
            newProjectile2.transform.position = transform.position;
            GameObject newProjectile3 = Instantiate(flames);
            newProjectile3.transform.position = transform.position;
        }
        /*if (Input.GetKey(KeyCode.Alpha1))
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
        }*/
        #endregion attacks

        #region abilities
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
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash)
        {
            canDash = false;
            _rigidbody2D.AddForce(Vector2.right * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            StartCoroutine(dash());
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash)
        {
            canDash = false;
            _rigidbody2D.AddForce(Vector2.left * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            StartCoroutine(dash());
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash)
        {
            canDash = false;
            _rigidbody2D.AddForce(Vector2.up * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            StartCoroutine(dash());
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000 && canDash)
        {
            canDash = false;
            _rigidbody2D.AddForce(Vector2.down * 20f, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            StartCoroutine(dash());
        }

        //consume
        if (Input.GetKeyDown(KeyCode.Q) && canConsume && GameController.spirits > 1000)
        {
            health += 300;
            GameController.spirits -= 1000;
            canConsume = false;
            HealthBar.fillAmount = health / maxHealth;
            StartCoroutine(Consume());
        }

        // Regenerate spirit
        if (spirit < 0 && freezeflag != true)
        {
            spirit = 0;
        }
        if (spirit <= spiritMax && freezeflag != true)
        {
            spirit += 500f * Time.deltaTime;
        }
        if (spirit > spiritMax)
        {
            spirit = spiritMax;
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
        #endregion abilities

        // Inventory
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (inventory[0])
            {
                inventory[0].use();
            }
        }

#region transform
        // transforming
        if ((Input.GetKeyDown(KeyCode.G)))
        {
            Transformation();
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
        #endregion transform

        // damage indication 
        {
            if (iframes > 0)
            {
                for (int i = 0; i < _spriteRenderer.Length; i++)
                {
                    _spriteRenderer[i].color = Color.red;
                }
            }
            else
            {
                for (int i = 0; i < _spriteRenderer.Length; i++)
                {
                    _spriteRenderer[i].color = Color.white;
                }
            }
        }
    }
    public void heal()
    {
        health = maxHealth;
    }

    private void FixedUpdate() // framerate locking rapid fire weapons
    {
        #region attacks2
        /*if (Input.GetKey(KeyCode.Alpha6))
        {
            weapon_equipped = 5;
        }
        if (weapon_equipped == 5 && Input.GetMouseButton(0))
        {
            GameObject newProjectile = Instantiate(Lightningprefab);
            newProjectile.transform.position = transform.position;
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            weapon_equipped = 6;
        }
        if (weapon_equipped == 6 && Input.GetMouseButton(0))
        {
            GameObject newProjectile = Instantiate(Cataclysmprefab);
            newProjectile.transform.position = transform.position;
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            weapon_equipped = 7;
        }
        if (weapon_equipped == 7 && Input.GetMouseButton(0))
        {
            GameObject newProjectile = Instantiate(Penumbraprefab);
            newProjectile.transform.position = transform.position;
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            weapon_equipped = 8;
        }
        if (weapon_equipped == 8 && Input.GetMouseButtonDown(0))
        {
            GameObject newProjectile = Instantiate(flames);
            newProjectile.transform.position = transform.position;
            GameObject newProjectile2 = Instantiate(flames);
            newProjectile2.transform.position = transform.position;
            GameObject newProjectile3 = Instantiate(flames);
            newProjectile3.transform.position = transform.position;
        }*/
        #endregion attacks2
    }

    #region weaponswaps
    public GameObject sword, shadebringer, hellbourne;
    public void Show()
    {
        // make it so that we go back to the greeting upon reopening
        ShowSword();
        sword.SetActive(true);
        shadebringer.SetActive(false);
        hellbourne.SetActive(false);
    }
    public void Hide()
    {
        gameObject.gameObject.SetActive(false);
    }
    void switchMenu(GameObject someMenu)
    {
        //clean up
        sword.SetActive(false);
        hellbourne.SetActive(false);
        shadebringer.SetActive(false);

        //activate requested menu
        someMenu.SetActive(true);
    }

    public void ShowSword() // calling it like this prevents erroneously calling the wrong game object
    {
        switchMenu(sword);
    }
    public void ShowHellbourne()
    {
        switchMenu(hellbourne);
    }
    public void ShowShadebringer()
    {
        switchMenu(shadebringer);
    }
    #endregion weaponswaps

    

    public void Transformation()
    {

        if (GameController.instance.activeChar)
        {
            animator.SetBool("activechar", true);
            Khealth.instance.off();
            Shealth.instance.on();
            SpiritK.instance.off();
            SpiritS.instance.on();
        }
    }

    IEnumerator dash()
    {
        yield return new WaitForSeconds(1);
        canDash = true;
    }

    IEnumerator Consume()
    {
        yield return new WaitForSeconds(45);
        canConsume = true;
    }
    IEnumerator flight()
    {
        flightcheck = false;
        Audiocontroller.instance.playflight();
        yield return new WaitForSeconds(0.35f);
        flightcheck = true;
    }
    #region upgrades
    public void Buyflight()
    {
        int cost = 100 + Mathf.RoundToInt((flight_new) * 2);
        if (GameController.spirits >= cost)
        {
            GameController.spirits -= cost;

            flight_new = flight_new + 100f;
            Samurai.instance.flight_new = Samurai.instance.flight_new + 100f;
            wingupgrade.text = "Spirit Essence: " + cost;
        }
    }

    public void Buyhealth()
    {
        int cost = 100 + Mathf.RoundToInt(maxHealth);

        if (GameController.spirits >= cost && health > 0)
        {
            GameController.spirits -= cost;

            health += 50;
            maxHealth += 50;
            HealthBar.fillAmount = health / maxHealth;

            Samurai.instance.boughthealth();


            healthupgrade.text = "Spirit Essence: " + cost;
        }
    }

    public void Buyspirit()
    {
        int cost = 100 + Mathf.RoundToInt(spiritMax);

        if (GameController.spirits >= cost)
        {
            GameController.spirits -= cost;

            spiritMax += 1000;
            Spiritbar.fillAmount = spiritMax;

            Samurai.instance.spiritMax += 1000;
            Samurai.instance.SpiritbarS.fillAmount = Samurai.instance.spiritMax;

            spiritupgrade.text = "Spirit Essence: " + cost;
        }
    }
    public void BuyHellbourne()
    {
        int cost = 10000;

        if (GameController.spirits >= cost && unlockedHellbourne != true)
        {
            GameController.spirits -= cost;
            unlockedHellbourne = true;
        }
    }
    public void BuyShadebringer()
    {
        int cost = 5000;

        if (GameController.spirits >= cost && unlockedShadebringer != true)
        {
            GameController.spirits -= cost;
            unlockedShadebringer = true;
        }
    }
    #endregion upgrades

    // check player jumping to prevent air jumps

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);
            //Debug.DrawRay(transform.position, Vector2.down * 2f);
            for(int i = 0; i < hits.Length; i++){
                RaycastHit2D hit = hits[i];
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    jump_counter = 1;
                    flight_time = 200f + flight_new;
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
