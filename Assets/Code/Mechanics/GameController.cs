using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Texture2D crosshair;
    public float maxStarDelay = 2f;
    public float minStarDelay = 0.2f;
    public float starDelay;

    public Transform[] spawnPoints;
    public GameObject[] starPrefabs;
    
    public float timeElapsed;

    //swap
    public GameObject Activeplayer, Samurai, Knight;
    public bool activeChar;

    void Awake(){
        instance = this;
    }
    void Start(){
        StartCoroutine("StarSpawnTimer");
        Cursor.SetCursor(crosshair, new UnityEngine.Vector2(100, 100), CursorMode.Auto);
        activeChar = false;
        Samurai.SetActive(!gameObject.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        float decreaseDelayOverTime = maxStarDelay - ((maxStarDelay - minStarDelay) / 30f * timeElapsed);
        starDelay = Mathf.Clamp(decreaseDelayOverTime, minStarDelay, maxStarDelay);
        
        
        //Swapping characters
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (activeChar == true)
            {
                Knight.SetActive(gameObject.activeInHierarchy);
                Samurai.SetActive(!gameObject.activeInHierarchy);
                Knight.transform.position = Samurai.transform.position;
                activeChar = false;
            }
            else if (activeChar == false) 
            {
                Samurai.SetActive(gameObject.activeInHierarchy);
                Knight.SetActive(!gameObject.activeInHierarchy);
                Samurai.transform.position = Knight.transform.position;
                activeChar = true;
            }
            
            
            /*if (activeChar == true)
            {
                Activeplayer.SetActive(activeChar);
                Knight.transform.postion = Samurai.transform.postion;
                
            }
            if (player2.gameobject.activeinhierarchy)
            {
                player1gameobject.transform.postion = player2gameobject.transform.postion;
            }*/
            //Player.SetActive(!gameObject.activeInHierarchy)
        }
        
    }

    void SpawnStar(){
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
        int randomStarIndex = Random.Range(0, starPrefabs.Length);
        GameObject randomStarPrefab = starPrefabs[randomStarIndex];
    
    Instantiate(randomStarPrefab, randomSpawnPoint.position, UnityEngine.Quaternion.identity);
    }
    IEnumerator StarSpawnTimer(){
        yield return new WaitForSeconds(starDelay);
        SpawnStar();
        StartCoroutine("StarSpawnTimer");
    }
}
