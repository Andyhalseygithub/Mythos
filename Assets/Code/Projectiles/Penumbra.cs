using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penumbra : baseprojectile
{
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    float randomAngle;
    public float angleToMouse;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(35f, 70f);
        //randomAngle = Random.Range(-0.1f, .1f);
        StartCoroutine(ChangeAngle());
        damage = 80;
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
        //randomAngle = Random.Range(-100f, 100f);
        float angle = randomAngle * Mathf.Rad2Deg;
        _rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, (angleToMouse - randomAngle));
        _rigidbody2D.velocity = transform.right * randomSpeed;
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
