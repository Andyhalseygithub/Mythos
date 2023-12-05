using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Texture2D crosshair;
    public float maxStarDelay = 2f;
    public float minStarDelay = 0.2f;
    public float starDelay;
    public TMP_Text spiritstext;
    public static int spirits;
    //public static bool unlockedHellbourne;
    //public static bool unlockedShadebringer;

    public Transform[] spawnPoints;
    public GameObject[] starPrefabs;

    public float timeElapsed;

    //swap
    public GameObject Activeplayer, Samurai, Knight;
    public bool activeChar;
    public bool currentcam;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine("StarSpawnTimer");
        Cursor.SetCursor(crosshair, new UnityEngine.Vector2(100, 100), CursorMode.Auto);
        activeChar = true;
        currentcam = true; //true for knight, false for samurai
        Samurai.SetActive(!gameObject.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        float decreaseDelayOverTime = maxStarDelay - ((maxStarDelay - minStarDelay) / 30f * timeElapsed);
        starDelay = Mathf.Clamp(decreaseDelayOverTime, minStarDelay, maxStarDelay);
        UpdateDisplay();

        //Swapping characters



        /*
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
        }
        */
        //Player.SetActive(!gameObject.activeInHierarchy)
    }
 
    public void TransformationS()
    {
        if (activeChar)
        {
            Samurai.SetActive(gameObject.activeInHierarchy);
            Knight.SetActive(!gameObject.activeInHierarchy);
            Samurai.transform.position = Knight.transform.position;
            activeChar = false;
            StartCoroutine(waiting());
            StartCoroutine(cameraswapS());
        }
    }
    public void TransformationK()
    {
        if (activeChar)
        {
            Knight.SetActive(gameObject.activeInHierarchy);
            Samurai.SetActive(!gameObject.activeInHierarchy);
            Knight.transform.position = Samurai.transform.position;
            activeChar = false;
            StartCoroutine(waiting());
            StartCoroutine(cameraswapK());
        }
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(10);
        yield return activeChar = true;
    }
    IEnumerator cameraswapS()
    {
        yield return new WaitForSeconds(0.01f);
        yield return currentcam = false;
    }
    IEnumerator cameraswapK()
    {
        yield return new WaitForSeconds(0.01f);
        yield return currentcam = true;
    }

    void SpawnStar()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndex];
        int randomStarIndex = Random.Range(0, starPrefabs.Length);
        GameObject randomStarPrefab = starPrefabs[randomStarIndex];

        Collider2D[] hits = Physics2D.OverlapCircleAll(randomSpawnPoint.position, .5f);
        bool spawnenemy = true;
        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D hit = hits[i];
            if (hit.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                spawnenemy = false;
            }
        }
        if (spawnenemy)
        {
            Instantiate(randomStarPrefab, randomSpawnPoint.position, Quaternion.identity);
        }
    }
    IEnumerator StarSpawnTimer()
    {
        yield return new WaitForSeconds(starDelay);
        SpawnStar();
        StartCoroutine("StarSpawnTimer");
    }

    public void GetSpirits(int spiritsAdded)
    {
        spirits += spiritsAdded;
    }
    void UpdateDisplay()
    {
        spiritstext.text = "Spirit Essence: \n" + spirits;
    }
}
