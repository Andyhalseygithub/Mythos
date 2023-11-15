using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    //outlets
    public GameObject mainMenu, optionsMenu, levelMenu;
    //public GameObject Activeplayer, Samurai, Knight;

    //methods
    private void Awake()
    {
        instance = this;
    }
    public void Show()
    {
        // make it so that we go back to the main menu upon reopening
        ShowMainMenu();
        gameObject.SetActive(true);
        // pause time
        Time.timeScale = 0;
        Samurai.instance.paused = true; // pause player
        Knight.instance.paused = true; // pause player
    }
    public void Hide()
    {
        gameObject.gameObject.SetActive(false);
        // start time
        Time.timeScale = 1f;
        Samurai.instance.paused = false; // pause player
        Knight.instance.paused = false; // pause player
        //Playercontroller.Instance.isPaused = false;
        // unpause player, we can't just do that, we need to check if player controller is actually there first
        //1st solution
        /*if (Playercontroller.Instance) {
            Playercontroller.Instance.isPaused = false;
        }*/
        //Playercontroller.Instance.isPaused = false;
    }
    //2nd solution 
    void Start()
    {
        Hide(); // runs after awake to make sure the player controller object exists
    }
    void switchMenu(GameObject someMenu)
    {
        //clean up
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelMenu.SetActive(false);

        //activate requested menu
        someMenu.SetActive(true);
    }

    public void ShowMainMenu() // calling it like this prevents erroneously calling the wrong game object
    {
        switchMenu(mainMenu);
    }
    public void ShowOptionsMenu()
    {
        switchMenu(optionsMenu);
    }
    public void ShowLevelMenu()
    {
        switchMenu(levelMenu);
    }
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}