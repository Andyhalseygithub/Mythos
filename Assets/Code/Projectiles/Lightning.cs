using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : baseprojectile
{
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    float randomAngle;
    public float angleToMouse;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(10f, 50f);
        //randomAngle = Random.Range(-0.1f, .1f);
        StartCoroutine(ChangeAngle());
        damage = 10;
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
        randomAngle = Random.Range(-30f, 30f);
        float angle = randomAngle * Mathf.Rad2Deg;
        _rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, (angleToMouse-randomAngle));
        _rigidbody2D.velocity = transform.right * randomSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator ChangeAngle()
    {
        randomAngle = Random.Range(-30f, 30f);
        yield return new WaitForSeconds(0.20f);
    }
}

// |
// \/  Projectile that follows mouse position
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : baseprojectile
{
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    float randomAngle;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(10f, 50f);
        //randomAngle = Random.Range(-0.1f, .1f);
        StartCoroutine(ChangeAngle());
        damage = 10;
    }
    //                                 |
    //  AWESOME MAGIC/LIGHTNING EFFECT \/
    void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        //randomAngle = Random.Range(-0.5f, 0.5f);
        float angle = randomAngle * Mathf.Rad2Deg;
        _rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, (angleToMouse-randomAngle));
        _rigidbody2D.velocity = transform.right * randomSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator ChangeAngle()
    {
        randomAngle = Random.Range(-5f, 5f);
        yield return new WaitForSeconds(0.20f);
    }
}
*/