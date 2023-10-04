using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.SceneManagement;

public class Shogunslash : Entity
{
    Rigidbody2D _rigidbody2D;
    public float timer = 7500;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * -15f;
        damage = 40;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 10000f * Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Playercontrol>())
        {
            Destroy(gameObject);
        }
    }
}
