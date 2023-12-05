using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flames : baseprojectile
{
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    float randomAngle;
    public float angleToMouse;
    public float count;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //randomSpeed = Random.Range(35f, 70f);
        //randomAngle = Random.Range(-0.1f, .1f);
        StartCoroutine(Burst());
        damage = 5;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        angleToMouse = radiansToMouse * Mathf.Rad2Deg;
    }

    //                                 |
    //  AWESOME MAGIC/LIGHTNING EFFECT \/
    void FixedUpdate()
    {
        float angle = randomAngle * Mathf.Rad2Deg;
        _rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, (angleToMouse - randomAngle));
        _rigidbody2D.velocity = transform.right * randomSpeed;
        count += 1;
        if (count > 50)
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator ChangeAngle()
    {
        randomAngle = Random.Range(-50f, 50f);
        randomSpeed = Random.Range(20f, 40f);
        yield return new WaitForSeconds(0.10f);
        yield return StartCoroutine(Burst());
    }
    IEnumerator Burst()
    {
        randomAngle = Random.Range(-10f, 10f);
        randomSpeed = 90f;


        yield return new WaitForSeconds(0.02f);
        yield return StartCoroutine(ChangeAngle());
    }
}
