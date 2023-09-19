using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D _rigidbody2D;
    float randomSpeed;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        randomSpeed = Random.Range(0.5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = Vector2.left * randomSpeed;
    }
    
    void OnBecameInvisible(){
        Destroy(gameObject);
    }
    
}
