using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class Merchant : MonoBehaviour
{
    public static Merchant instance;
    SpriteRenderer _spriteRenderer;
    Transform target;
    //public GameObject item1, item2, item3;
    //public GameObject Continue, ItemMenu;
    void Awake()
    {
        instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        if (transform.position.x > target.position.x)
        {
            //transform.localScale = new Vector3(-5, 5, 5);
            _spriteRenderer.flipX = true;
            //print("Right");
        }
        else if (transform.position.x < target.position.x)
        {
            //transform.localScale = new Vector3(5, 5, 5);
            _spriteRenderer.flipX = false;
            //print("Left");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Playerbase>() && Input.GetKeyDown(KeyCode.E))
        {
            print("e");
            Audiocontroller.instance.playmenu();
            MerchantMenu.instance.Show();
        }
    }


}
