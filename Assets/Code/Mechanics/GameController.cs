using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int spirits;

    public Transform[] spawnPoints;
    public GameObject[] starPrefabs;

    public float timeElapsed;

    //swap
    public GameObject Activeplayer, Samurai, Knight;
    public bool activeChar;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine("StarSpawnTimer");
        Cursor.SetCursor(crosshair, new UnityEngine.Vector2(100, 100), CursorMode.Auto);
        activeChar = false;
        Samurai.SetActive(!gameObject.activeInHierarchy);
        spirits = 0;
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
        Samurai.SetActive(gameObject.activeInHierarchy);
        Knight.SetActive(!gameObject.activeInHierarchy);
        Samurai.transform.position = Knight.transform.position;
        activeChar = true;
    }
    public void TransformationK()
    {
        Knight.SetActive(gameObject.activeInHierarchy);
        Samurai.SetActive(!gameObject.activeInHierarchy);
        Knight.transform.position = Samurai.transform.position;
        activeChar = false;
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
        spiritstext.text = "Spirit Essence: " + spirits;
    }
}
