using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windmovement : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    float randomAngle;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(10f, 30f);
        randomAngle = Random.Range(-0.1f, .1f);
        StartCoroutine(ChangeAngle());
    }
    //                                 |
    //  AWESOME MAGIC/LIGHTNING EFFECT \/
    /*void FixedUpdate()
    {
        randomAngle = Random.Range(0, 180);
        float angle = randomAngle * Mathf.Rad2Deg;
        _rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, angle);
        _rigidbody2D.velocity = transform.right * -randomSpeed;
    }*/
    void FixedUpdate()
    {
        StartCoroutine(ChangeAngle());
        float angle = randomAngle * Mathf.Rad2Deg;
        //print(angle);
        _rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, angle);
        _rigidbody2D.velocity = transform.right * -randomSpeed;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator ChangeAngle()
    {
        randomAngle = Random.Range(-0.1f, .1f);
        yield return new WaitForSeconds(0.20f);
    }
}
