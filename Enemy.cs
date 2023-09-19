using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Start is called before the first frame update
    float acceleration = 1f;
    float maxSpeed = 2f;
    Rigidbody2D _rigidbody2D;
    Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //float acceleration = 1f;
        //float maxSpeed = 2f;
        ChaseAI();
        if(target != null){
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            _rigidbody2D.MoveRotation(angle);
        }
        _rigidbody2D.AddForce(transform.right * acceleration);
        _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, maxSpeed);
    }
    void ChaseAI(){
        target = GameObject.Find("Player").transform;
    }
    
}
