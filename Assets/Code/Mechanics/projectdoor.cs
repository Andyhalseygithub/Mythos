using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class projectdoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicController.instance.playtowertheme();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Playerbase>())
        {
            Samurai.instance.heal();
            Knight.instance.heal();
            SceneManager.LoadScene("Forest");
        }
    }
}
