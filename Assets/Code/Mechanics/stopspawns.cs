using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopspawns : MonoBehaviour
{
    public bool healedPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Playerbase>())
        {
            if (healedPlayer == false)
            {
                GameController.instance.starDelay = 100000000;
                GameController.instance.maxStarDelay = 100000000;
                GameController.instance.minStarDelay = 100000000;
                Knight.instance.heal();
                Samurai.instance.heal();
                /*GameObject spawnboss = Instantiate(Shogun);
                spawnboss.transform.position = transform.position + new Vector3(5f, 20f, 0);*/
                healedPlayer = true;
            }
        }
    }
}
