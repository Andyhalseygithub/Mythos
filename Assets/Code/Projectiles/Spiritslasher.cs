using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Spiritslasher : baseprojectile
{
    public Transform target;
    public int collisionCounter;
    Rigidbody2D _rigidbody2D;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        damage = 25;
    }
    void Update()
    {
        if(collisionCounter >= 5)
        {
            Destroy(gameObject);
        }
        ChooseNearestTarget();
        if (target != null)
        {
            // rotate towards target
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            _rigidbody2D.MoveRotation(angle);
            _rigidbody2D.velocity = transform.right * 25f;
        }
        else {
            _rigidbody2D.velocity = -transform.right * 25f;
        }
        /*for (int i = 100; i < 220; i += 5)
        {
            transform.rotation = Quaternion.Euler(0, 0, i);
            if (i == 220)
            {
                i = 100;
            }
        }*/

        /*Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mousePositionInWorld;
        Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

        float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
        float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

        aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);*/
    }
    void ChooseNearestTarget()
    {
        
        float closestDistance = 9999f; 

        Entity[] entities = FindObjectsOfType<Entity>();
                                                              

        
        for (int i = 0; i < entities.Length; i++) 
        {
            Entity entity = entities[i];
            //asteroid has to be to the right of the missile
            if (entity.transform.position.x > transform.position.x)
            {
                Vector2 directionToTarget = entity.transform.position - transform.position;

                
                if (directionToTarget.sqrMagnitude < closestDistance) 
                {
                    closestDistance = directionToTarget.sqrMagnitude;
                }
                
                target = entity.transform;
            }
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Entity>()){
            collisionCounter += 1;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity>())
        {
            collisionCounter += 1;
        }
    }
}
