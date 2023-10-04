using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class annihiliationexplosion : baseprojectile
{
    // Start is called before the first frame update
    Rigidbody2D _rigidbody2D;
    public float timer = 10000;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        damage = 2500;
        //_rigidbody2D.AddRelativeForce(Vector2.right * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1000f * Time.deltaTime;
        /*while (timer >= 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }*/
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        
    }
}
