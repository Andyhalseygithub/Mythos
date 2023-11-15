using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class wind : MonoBehaviour
{
    public static wind instance;
    public float maxwindDelay = 2f;
    public float minwindDelay = 0.2f;
    public float windDelay;

    public Transform[] windSpawnPoints;
    public GameObject[] windPrefabs;

    public float timeElapsed;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine("WindSpawnTimer");
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        float decreaseDelayOverTime = maxwindDelay - ((maxwindDelay - minwindDelay) / 30f * timeElapsed);
        windDelay = Mathf.Clamp(decreaseDelayOverTime, minwindDelay, maxwindDelay);
    }
    void SpawnWind()
    {
        int randomSpawnIndex = Random.Range(0, windSpawnPoints.Length);
        Transform randomSpawnPoint = windSpawnPoints[randomSpawnIndex];
        int randomWindIndex = Random.Range(0, windPrefabs.Length);
        GameObject randomWindPrefab = windPrefabs[randomWindIndex];        
        Instantiate(randomWindPrefab, randomSpawnPoint.position, Quaternion.identity);
        
    }
    IEnumerator WindSpawnTimer()
    {
        yield return new WaitForSeconds(windDelay);
        SpawnWind();
        StartCoroutine("WindSpawnTimer");
    }
}