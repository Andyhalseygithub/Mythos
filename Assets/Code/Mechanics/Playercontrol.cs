using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class Playercontrol : MonoBehaviour
{
    //Initialize playercontrol as an object for enemies to track
    public static Playercontrol instance;
    // initialize health and spirit vars
    public TMP_Text thealth;
    public int health;
    public TMP_Text tspirit;
    public float spirit;
    // Initializing jumps and flighttime vars
    public int jump_counter;
    public float flight_time;
    // Initialize rigidbody for usage in code
    Rigidbody2D _rigidbody2D;
    //get transform and projectile code for player usage
    public Transform aimPivot;
    public GameObject testprojectilePrefab;

    //bool variable for jumps
    bool midair;
    
    //Health
    void Awake(){
        instance = this;
    }


    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //health
        //thealth.text = health.ToString();



        //if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){}

        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
        _rigidbody2D.AddForce(Vector2.down * 15f * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
        _rigidbody2D.AddForce(Vector2.left * 15f * Time.deltaTime, ForceMode2D.Impulse); // vector(x,y,z)
        }

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
        _rigidbody2D.AddForce(Vector2.right * 15f * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
        }
        
        // Jump
        if(Input.GetKey(KeyCode.Space) && jump_counter > 0 &! midair){
        jump_counter --;
        _rigidbody2D.AddForce(Vector2.up * 3f, ForceMode2D.Impulse); // vector(x,y,z)
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
        if(Input.GetKey(KeyCode.Space) && jump_counter == 0){
            if(flight_time > 0){
                flight_time -= 100f * Time.deltaTime;
                _rigidbody2D.AddForce(Vector2.up * .06f, ForceMode2D.Impulse);

            }
        }

        // Aim
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

        //Attack
        if(Input.GetMouseButtonDown(0)){
            GameObject newProjectile = Instantiate(testprojectilePrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
        }

        //freeze ability 
        if(Input.GetKey(KeyCode.F) && spirit > 0){
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            //decrement spirit while using ability
            spirit -= 1f * Time.deltaTime;
        }
        else
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Spirit dash ability
        if(Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift) && spirit > 0 || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && spirit > 0){
        _rigidbody2D.AddForce(Vector2.right * 1000f * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 10f / Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift) && spirit > 0 || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && spirit > 0)
        {
        _rigidbody2D.AddForce(Vector2.left * 1000f * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 10f / Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift) && spirit > 0 || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && spirit > 0){
        _rigidbody2D.AddForce(Vector2.up * 1000f * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 10f / Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftShift) && spirit > 0 || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) && spirit > 0){
        _rigidbody2D.AddForce(Vector2.down * 1000f * Time.deltaTime, ForceMode2D.Impulse);  // vector(x,y,z)
            spirit -= 10f * Time.deltaTime;
        }
    }

    // check player jumping to prevent air jumps

    void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);
            //Debug.DrawRay(transform.position, Vector2.down * 2f);
            for(int i = 0; i < hits.Length; i++){
                RaycastHit2D hit = hits[i];
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    jump_counter = 2;
                    flight_time = 2000f;
                    midair = false;
                }
                else
                {
                    midair = true;
                }
            }
        }
    }
}
