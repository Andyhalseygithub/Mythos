using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playershogunsword : baseprojectile
{
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    float randomAngle;
    public float angleToMouse;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //randomSpeed = Random.Range(35f, 70f);
        //randomAngle = Random.Range(-0.1f, .1f);
        //StartCoroutine(ChangeAngle());
        //damage = 80;
        /*Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        angleToMouse = radiansToMouse * Mathf.Rad2Deg;*/
    }
    //                                 |
    //  AWESOME MAGIC/LIGHTNING EFFECT \/
    void FixedUpdate()
    {
        //randomAngle = Random.Range(-100f, 100f);
        //float angle = randomAngle * Mathf.Rad2Deg;
        //_rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, (angleToMouse - randomAngle));
        _rigidbody2D.velocity = -transform.up * 40f; //randomSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator ChangeAngle()
    {
        randomAngle = Random.Range(-40f, 40f);
        yield return new WaitForSeconds(0.10f);
    }
}


    /*// Start is called before the first frame update
    Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = -transform.up * 10f;
        damage = 50;
    //_rigidbody2D.AddRelativeForce(Vector2.right * 10f);
}

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
  
    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity += _rigidbody2D.velocity * 2f * Time.deltaTime;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }*/

