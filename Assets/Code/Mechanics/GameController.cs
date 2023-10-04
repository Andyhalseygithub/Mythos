using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake(){
        instance = this;
    }
    void Start(){
        StartCoroutine("StarSpawnTimer");
        Cursor.SetCursor(crosshair, new Vector2(100, 100), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        float decreaseDelayOverTime = maxStarDelay - ((maxStarDelay - minStarDelay) / 30f * timeElapsed);
        starDelay = Mathf.Clamp(decreaseDelayOverTime, minStarDelay, maxStarDelay);
    }

    void SpawnStar(){
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
        int randomStarIndex = Random.Range(0, starPrefabs.Length);
        GameObject randomStarPrefab = starPrefabs[randomStarIndex];
    
    Instantiate(randomStarPrefab, randomSpawnPoint.position, Quaternion.identity);
    }
    IEnumerator StarSpawnTimer(){
        yield return new WaitForSeconds(starDelay);
        SpawnStar();
        StartCoroutine("StarSpawnTimer");
    }
}
