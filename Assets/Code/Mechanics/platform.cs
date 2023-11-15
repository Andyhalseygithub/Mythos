using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class platform : MonoBehaviour
{
    BoxCollider2D _boxCollider;
    PlatformEffector2D _effector;
    public GameObject Activeplayer, Samurai, Knight;
    public Collider2D ColliderS;
    public Collider2D ColliderK;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _effector = GetComponent<PlatformEffector2D>();
        ColliderS = Samurai.GetComponent<CapsuleCollider2D>();
        ColliderK = Knight.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionStay2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Playerbase>() && Input.GetKey(KeyCode.S))
        {
            StartCoroutine(DisableCollision());
        }
    }
    IEnumerator DisableCollision()
    {
        Physics2D.IgnoreCollision(_boxCollider, ColliderK);
        Physics2D.IgnoreCollision(_boxCollider, ColliderS);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(_boxCollider, ColliderK, false);
        Physics2D.IgnoreCollision(_boxCollider, ColliderS, false);
    }
}
