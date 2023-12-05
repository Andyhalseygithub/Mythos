using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAltar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Shogun;
    public bool SpawnedBoss = false;
    void Start()
    {
        
    }


    void OnCollisionStay2D(Collision2D other) // gets the other object colliding with this object
    {
        if (other.gameObject.GetComponent<Playerbase>()) {
            if (SpawnedBoss == false) {
                Shogun.SetActive(true);
                /*GameObject spawnboss = Instantiate(Shogun);
                spawnboss.transform.position = transform.position + new Vector3(5f, 20f, 0);*/
                SpawnedBoss = true;
            }
        }
    }
}
