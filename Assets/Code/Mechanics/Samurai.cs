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

public class Samurai : Playerbase
{
    #region variables
    //get animator
    Animator animator;
    //Initialize playercontrol as an object for enemies to track
    public static Samurai instance;
    // initialize health and spirit vars
    public TMP_Text thealth;
    public static float health;
    public static float maxHealth;
    public TMP_Text tspirit;
    public float spirit;
    public float spiritMax;
    public UnityEngine.UI.Image SpiritbarS;
    public bool freezeflag;
    // inventory variables 
    public float weapon_equipped = 0;
    // Initializing jumps and flighttime vars
    public int jump_counter;
    public float flight_time;
    public float flight_new;
    // Initialize rigidbody for usage in code
    Rigidbody2D _rigidbody2D;
    //get transform and projectile code for player usage
    public Transform aimPivot;
    public Transform aimPivot2;
    public GameObject testprojectilePrefab;
    public GameObject playershogunswordprefab;
    public GameObject meleeblade;
    public GameObject annihilationray;
    public GameObject Spiritslasher;
    public GameObject TendrilPrefab;
    // Rotation timer
    public float i = 100;

    // Inventory
    public Item[] inventory;
    public int activeslot;
    //Health
    public UnityEngine.UI.Image SHealthBar;
    //Invincibility frames
    public float iframes;

    //Speed and acceleration
    public float speedX = 60f;
    public float maxSpeedX = 25f;
    public float speedY = 70f;
    public float maxSpeedY = 25f;

    // Call sprite renderer
    SpriteRenderer[] _spriteRenderer;

    //menus
    public bool paused;

    //upgrade texts
    public TMP_Text healthupgrade;
    public TMP_Text wingupgrade;
    public TMP_Text spiritupgrade;
    public bool unlockedPenumbra;
    public bool unlockedBlizzard;

    //dash 
    public bool canDash;

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
        health = 300;
        maxHealth = 300;
        canDash = true;
        canConsume = true;
        spirit = 1000;
        spiritMax = 1000;
        unlockedPenumbra = false;
        unlockedBlizzard = false;
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
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
        _rigidbody2D.AddForce(Vector2.down * speedY * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
        _rigidbody2D.AddForce(Vector2.left * speedX * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)

            // sprite direction
            //transform.localScale = new Vector3(-2,2,2);
            //_spriteRenderer.flipX = true;

        }

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
        _rigidbody2D.AddForce(Vector2.right * speedX * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            // sprite direction
            //transform.localScale = new Vector3(2, 2, 2);
            //_spriteRenderer.flipX = false;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && jump_counter > 0){
        jump_counter --;
        _rigidbody2D.AddForce(Vector2.up * 35f, ForceMode2D.Impulse); // vector(x,y,z)
        }

        if(Input.GetKey(KeyCode.Space) && jump_counter == 0){
            if(flight_time > 0){
                flight_time -= 100f * Time.deltaTime;
                _rigidbody2D.AddForce(Vector2.up * 150f * Time.deltaTime, ForceMode2D.Impulse);
                
            }
        }
        #endregion movement

        #region attacks
        // cursor
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;
        if (directionFromPlayerToMouse.x > 0)
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

        // katana Attack
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapon_equipped = 1;
            ShowKatana();
        }
        if (weapon_equipped == 1 && Sarmswinging.instance.LaunchProjectile == true)
        {
            GameObject newProjectile = Instantiate(meleeblade);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        //penumbra
        if (Input.GetKey(KeyCode.Alpha2) && unlockedPenumbra == true)
        {
            weapon_equipped = 2;
            ShowPenumbra();
        }

        //blizzard attack
        if (Input.GetKey(KeyCode.Alpha3) && unlockedBlizzard == true)
        {
            weapon_equipped = 3;
            ShowBlizzard();
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
        if (Input.GetKey(KeyCode.F) && spirit > 0){
            //_rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            //decrement spirit while using ability
            spirit -= 500f * Time.deltaTime;
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            freezeflag = true;
        }
        else if(Input.GetKey(KeyCode.F) && spirit <= 0)
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
        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit >= 1000 || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000){
        _rigidbody2D.AddForce(Vector2.right * speedX, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            canDash = false;
            //animator.SetBool("dash", true);
            StartCoroutine(dash());
        }
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit >= 1000 || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000)
        {
        _rigidbody2D.AddForce(Vector2.left * speedX, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            canDash = false;
            //animator.SetBool("dash", true);
            StartCoroutine(dash());
        }
        if(Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit >= 1000 || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000){
        _rigidbody2D.AddForce(Vector2.up * speedX, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            canDash = false;
            //animator.SetBool("dash", true);
            StartCoroutine(dash());
        }
        if(Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftShift) && spirit >= 1000 || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && spirit >= 1000){
        _rigidbody2D.AddForce(Vector2.down * speedX, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 1000f;
            canDash = false;
            //animator.SetBool("dash", true);
            StartCoroutine(dash());
        }

        if (Input.GetKeyDown(KeyCode.Q) && canConsume && GameController.spirits > 1000)
        {
            health += 300;
            GameController.spirits -= 1000;
            canConsume = false;
            SHealthBar.fillAmount = health / maxHealth;
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
        SpiritbarS.fillAmount = spirit / spiritMax;
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

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (inventory[9])
            {
                inventory[9].use();
            }
        }

        #region transform
        // animation
        if ((Input.GetKeyDown(KeyCode.G)))
        {
            Transformation();
        }

        //freeze player during transform
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

    public void boughthealth()
    {
        health += 50;
        maxHealth += 50;
        SHealthBar.fillAmount = health / maxHealth;
    }
    private void FixedUpdate() // framerate locking rapid fire weapons
    {
        /*if (Input.GetKey(KeyCode.Alpha6))
        {
            weapon_equipped = 5;
        }
        if (weapon_equipped == 5 && Input.GetMouseButton(0))
        {
            GameObject newProjectile = Instantiate(TendrilPrefab);
            newProjectile.transform.position = transform.position;
        }*/
    }
    public void Transformation()
    {
        if (GameController.instance.activeChar)
        {
            animator.SetBool("activechar", true);
            Khealth.instance.on();
            Shealth.instance.off();
            SpiritK.instance.on();
            SpiritS.instance.off();
        }
    }

    #region weaponswaps
    public GameObject katana, blizzard, penumbra;
    public void Show()
    {
        // make it so that we go back to the greeting upon reopening
        ShowKatana();
        katana.SetActive(true);
        blizzard.SetActive(false);
        penumbra.SetActive(false);
    }
    public void Hide()
    {
        gameObject.gameObject.SetActive(false);
    }
    void switchMenu(GameObject someMenu)
    {
        //clean up
        katana.SetActive(false);
        blizzard.SetActive(false);
        penumbra.SetActive(false);

        //activate requested menu
        someMenu.SetActive(true);
    }

    public void ShowKatana() // calling it like this prevents erroneously calling the wrong game object
    {
        switchMenu(katana);
    }
    public void ShowBlizzard()
    {
        switchMenu(blizzard);
    }
    public void ShowPenumbra()
    {
        switchMenu(penumbra);
    }
    #endregion weaponswaps

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

    
    #region upgrades
    /*public void Buyflight()
    {
        int cost = 100 + Mathf.RoundToInt((flight_new) * 2);
        if (GameController.spirits >= cost)
        {
            GameController.spirits -= cost;

            flight_new = flight_new + 100f;
            wingupgrade.text = "Spirits: " + cost;
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
            SHealthBar.fillAmount = health / maxHealth;

            wingupgrade.text = "Spirits: " + cost;
        }
    }

    public void Buyspirit()
    {
        int cost = 100 + Mathf.RoundToInt(spiritMax);

        if (GameController.spirits >= cost)
        {
            GameController.spirits -= cost;

            spiritMax += 1000;
            SpiritbarS.fillAmount = spiritMax;

            spiritupgrade.text = "Spirits: " + cost;
        }
    }*/

    public void BuyPenumbra()
    {
        int cost = 5000;

        if (GameController.spirits >= cost && unlockedPenumbra != true)
        {
            GameController.spirits -= cost;
            unlockedPenumbra = true;
        }
    }
    public void BuyBlizzard()
    {
        int cost = 10000;

        if (GameController.spirits >= cost && unlockedBlizzard != true)
        {
            GameController.spirits -= cost;
            unlockedBlizzard = true;
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
    void OnTriggerEnter2D(Collider2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Entity>() && iframes <= 0)
        {
            health -= other.gameObject.GetComponent<Entity>().damage;
            SHealthBar.fillAmount = health / maxHealth;
            iframes = 10000;
            if (health <= 0)
            {
                string currentScene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentScene);
            }
        }
    }
}
